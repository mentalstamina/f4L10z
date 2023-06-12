using LogisticsApi.DAL.Repositories;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace LogisticsApi.DAL.Models
{
    public class Package : IRepositoryEntity<string>
    {

        public Package(string kolliId)
        {
            KolliId = kolliId;
            Created = DateTime.Now;
        }
        [JsonConstructor]
        public Package(string kolliId, int height, int width, int length, int weight)
        {
            KolliId = kolliId;
            Height = height;
            Width = width;
            Length = length;
            Weight = weight;
            Created = DateTime.Now;

        }
        [RegularExpression(Limitations.KolliIdRegexPattern, ErrorMessage = KolliIdException.ErrorMessage)]
        public string KolliId { get; }
        [Range(0, Limitations.MaxHeight)]
        public int Height { get; set; }
        [Range(0, Limitations.MaxWidth)]
        public int Width { get; set; }
        [Range(0, Limitations.MaxLength)]
        public int Length { get; set; }
        [Range(0, Limitations.MaxWeight)]
        public int Weight { get; set; }
        [JsonIgnore]
        public DateTime Created { get; }
        [JsonIgnore]
        public string Id => KolliId;

        public bool IsValid =>
            Height <= Limitations.MaxHeight && Height >= 0 &&
            Width <= Limitations.MaxWidth && Width >= 0 &&
            Length <= Limitations.MaxLength && Length >= 0 &&
            Weight <= Limitations.MaxWeight && Weight >= 0;

        public static class Limitations
        {
            public const int MaxHeight = 60;
            public const int MaxWidth = 60;
            public const int MaxLength = 60;
            public const int MaxWeight = 20000;
            public const string KolliIdRegexPattern = "^999\\d{15}$";


        }
        public class KolliIdException : Exception
        {
            public const string ErrorMessage = "Invalid format, expecting 18 digits that starts with 999.";
            public KolliIdException() : base($"KolliId: {ErrorMessage}") { }

        }
    }
}