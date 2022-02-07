using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Platform.Utilities.HtmlParser
{
    public class JsonHtmlParser : IHtmlParser
    {
        private static string VARIABLE_REGEX = @"\[(.*?)\]";

        public async Task<string> ParseHtmlFile(string filePath, string json)
        {
            if (filePath == null)
                throw new ArgumentNullException("File path is required");

            if (!File.Exists(filePath))
                throw new InvalidOperationException($"File not found for parsing [{filePath}]");

            var fileContent = File.ReadAllText(filePath);

            if (String.IsNullOrEmpty(fileContent) || String.IsNullOrWhiteSpace(fileContent))
                throw new InvalidDataException($"Empty file. No html found for parsing [{filePath}]");

            return await ParseHtml(fileContent, json);

        }

        public async Task<string> ParseHtml(string html, string json)
        {
            return await Task.Run(() =>
            {
                //simple regex match for variables
                var matches = Regex.Matches(html, VARIABLE_REGEX);
                Console.WriteLine("there were {0} matches", matches.Count);

                if (matches.Count <= 0)
                    return html;

                //parse the json to JObject for name based traversal
                var data = (JObject)JsonConvert.DeserializeObject(json);

                //replace all the occurrences with provided json data
                foreach (Match match in matches)
                {
                    var key = match.Value.Replace("[", "").Replace("]", "");

                    if (String.IsNullOrEmpty(key) || String.IsNullOrWhiteSpace(key))
                        continue;

                    var d = data[key]; ;

                    if (d == null)
                        continue;

                    if (d.Type == JTokenType.String)
                        html = html.Replace(match.Value, d.Value<string>());
                }

                //replace all the tables with required objects
                //using HtmlAgilityPack

                var doc = new HtmlDocument();
                doc.LoadHtml(html.Replace("\r", string.Empty).Replace("\n", " "));
                
                //parsing for table manipulation
                var tableNodes = doc.DocumentNode.SelectNodes("//table");
                if (tableNodes == null)
                    return html;
                //get all tables
                foreach (var table in tableNodes)
                {

                    //get table attribute
                    var tableName = table.GetAttributeValue("accessKey", string.Empty);

                    if (String.IsNullOrEmpty(tableName))
                        continue;
                    
                    var rows = table.ChildNodes.Where(c=>c.Name.Equals("tr")).ToList();
                    var newRows = new HtmlNodeCollection(table);


                    if (rows.Count <= 0)
                        continue;

                    var headerRow = rows[0];
                    var dataRow = rows[1];

                    var keys = dataRow.ChildNodes.Where(c=>c.Name.Equals("td")).ToList();

                    if (keys.Count == 0)
                        continue;

                    //get this element from json 
                    var jsonObject = data[tableName];

                    if (jsonObject == null || jsonObject.Type != JTokenType.Array)
                        continue;

                    //use 'object's' properties for table column
                    foreach (var obj in jsonObject)
                    {
                        if (obj.Type != JTokenType.Object)
                            continue;

                        var newRow = dataRow.Clone();
                        newRow.RemoveAllChildren();

                        foreach (var node in keys)
                        {
                            var key = node.InnerText.Replace("[", "").Replace("]", "").Trim();

                            var value = "N/A";

                            if (!string.IsNullOrEmpty(key) && obj[key] != null)
                                value = obj[key].ToString();
                            else
                                continue;
                            
                            var newNode = node.Clone();
                            newNode.InnerHtml = value;

                            newRow.AppendChild(newNode);
                        }

                        table.AppendChild(newRow);
                    }

                    table.RemoveChild(dataRow);
                }
                
                //parsing for repeatable custom views with accessKey
                var repeatableElements = doc.DocumentNode.SelectNodes("//*[@key]");

                if (repeatableElements == null)
                    return doc.DocumentNode.OuterHtml;
                    
                foreach (var element in repeatableElements.Where(r=>!r.Name.Equals("table")).ToList())
                {
                    //get table attribute
                    var elementName = element.GetAttributeValue("key", string.Empty);

                    if (string.IsNullOrEmpty(elementName))
                        continue;
                    
                    //get this element from json 
                    var jsonObject = data[elementName];

                    if (jsonObject == null || jsonObject.Type != JTokenType.Array)
                        continue;
                    
                    var m = Regex.Matches(element.InnerHtml, VARIABLE_REGEX);
                    Console.WriteLine("there were {0} matches", matches.Count);

                    if (m.Count <= 0)
                        continue;

                    foreach (var jToken in jsonObject)
                    {
                        var newElement = element.CloneNode(true);
                        
                        foreach (Match match in m)
                        {
                            var key = match.Value.Replace("[", "").Replace("]", "");

                            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                                continue;

                            var d = jToken[key]; ;

                            if (d == null)
                                continue;

                            if (d.Type == JTokenType.String)
                                newElement.InnerHtml = newElement.InnerHtml.Replace(match.Value, d.Value<string>());
                        }

                        element.ParentNode.AppendChild(newElement);
                    }

                    //remove the element containing the structure
                    element.ParentNode.RemoveChild(element);
                }

                return doc.DocumentNode.OuterHtml;
            });
        }
    }
}
