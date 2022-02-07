using System.Threading.Tasks;

namespace Platform.Utilities.RiskAnalysisModel
{
    public interface IRiskAnalysisModel
    {
        Task<double> CalculateScore(double weight, int riskLevel);
        Task<string> CalculateRiskRating(int riskLevel);
    }
}
