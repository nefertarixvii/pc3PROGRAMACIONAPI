using ApiInteligenteTareas.MLModels;
using Microsoft.ML;

namespace ApiInteligenteTareas.Services
{
    public class SentimientoService
    {
        private readonly PredictionEngine<
            SentimientoData,
            SentimientoPrediction> _predictionEngine;

        public SentimientoService()
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<SentimientoData>(
                "Dataset/sentimientos.csv",
                hasHeader: true,
                separatorChar: ',');

            var pipeline =
                mlContext.Transforms.Text.FeaturizeText(
                    "Features",
                    nameof(SentimientoData.Texto))
                .Append(
                    mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                        labelColumnName:
                        nameof(SentimientoData.Sentimiento),
                        featureColumnName:
                        "Features"));

            var model = pipeline.Fit(data);

            _predictionEngine =
                mlContext.Model.CreatePredictionEngine
                <SentimientoData,
                SentimientoPrediction>(model);
        }

        public string Analizar(string comentario)
        {
            var prediction =
                _predictionEngine.Predict(
                    new SentimientoData
                    {
                        Texto = comentario
                    });

            return prediction.Prediccion
                ? "Positivo"
                : "Negativo";
        }
    }
}