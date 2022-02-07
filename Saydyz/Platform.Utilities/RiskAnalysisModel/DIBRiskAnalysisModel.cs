using System.Threading.Tasks;

namespace Platform.Utilities.RiskAnalysisModel
{
    public class SimpleRiskAnalysisModel : IRiskAnalysisModel
    {
        public async Task<double> CalculateScore(double weight, int riskLevel)
        {
            return await Task.Run(()=>weight * riskLevel);
        }

        public async Task<string> CalculateRiskRating(int riskLevel)
        {

            //this needs to be verified with zohaib
            return await Task.Run(() => {
                if (riskLevel > 300)
                    return "high_risk_rating";
                else if (riskLevel > 200 && riskLevel <= 300)
                    return "moderate_risk_rating";
                else
                    return "low_risk_rating";
            });
        }
    }
}
