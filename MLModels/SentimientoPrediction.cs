using Microsoft.ML.Data;

namespace ApiInteligenteTareas.MLModels
{
    public class SentimientoPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediccion { get; set; }
    }
}