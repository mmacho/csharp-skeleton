using Aseme.Shared.Domain.HttpLogs.Domain;
using Microsoft.EntityFrameworkCore;

namespace aseme_api.Infrastructure.Models.HubSuppliers
{
    public static partial class HttpLogExtension
    {
        public static ModelBuilder BuildHttpLog(this ModelBuilder modelBuilder)
        {
            var httpLogEntity = modelBuilder.Entity<HttpLog>();

            httpLogEntity.ToTable(HttpLog.TableName);
            httpLogEntity.HasKey(httpLog => httpLog.Id);

            httpLogEntity.HasIndex(httpLog => httpLog.ReceivedDateTime);
            httpLogEntity.HasIndex(httpLog => httpLog.IpAddress);
            httpLogEntity.HasIndex(httpLog => httpLog.HttpStatusCode);
            httpLogEntity.HasIndex(httpLog => httpLog.EntityId);

            httpLogEntity
                .Property(httpLog => httpLog.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            httpLogEntity
                .Property(httpLog => httpLog.ReceivedDateTime)
                .HasColumnName("ReceivedDateTime")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "ReceivedDateTime is required");

            // IpAddress (v4/v6)
            httpLogEntity
                .Property(httpLog => httpLog.IpAddress)
                .HasColumnName("IpAddress")
                .HasMaxLength(45);

            httpLogEntity
                .Property(httpLog => httpLog.Scheme)
                .HasColumnName("Scheme")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Scheme is required")
                .HasMaxLength(15);

            httpLogEntity
                .Property(httpLog => httpLog.HttpMethod)
                .HasColumnName("HttpMethod")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "HttpMethod is required")
                .HasMaxLength(15);

            httpLogEntity
                .Property(httpLog => httpLog.HttpPath)
                .HasColumnName("HttpPath")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "HttpPath is required")
                .HasMaxLength(500)
                .IsRequired();

            httpLogEntity
                .Property(httpLog => httpLog.HttpQueryParams)
                .HasColumnName("HttpQueryParams")
                .HasColumnType("nvarchar(MAX)");

            httpLogEntity
                .Property(httpLog => httpLog.HttpRequestHeaders)
                .HasColumnName("HttpRequestHeaders")
                .HasColumnType("nvarchar(MAX)");

            httpLogEntity
                .Property(httpLog => httpLog.HttpRequestBody)
                .HasColumnName("HttpRequestBody")
                .HasColumnType("nvarchar(MAX)");

            httpLogEntity
                .Property(httpLog => httpLog.HttpResponseHeaders)
                .HasColumnName("HttpResponseHeaders")
                .HasColumnType("nvarchar(MAX)");

            httpLogEntity
                .Property(httpLog => httpLog.HttpResponseBody)
                .HasColumnName("HttpResponseBody")
                .HasColumnType("nvarchar(MAX)");

            httpLogEntity
                .Property(httpLog => httpLog.HttpStatusCode)
                .HasColumnName("HttpStatusCode")
                .IsRequired()
                .HasAnnotation("ErrorMessage", "HttpStatusCode is required");

            httpLogEntity
                .Property(httpLog => httpLog.ExecutionTime)
                .HasColumnName("ExecutionTime");

            httpLogEntity
                .Property(httpLog => httpLog.EntityId)
                .HasColumnName("EntityId");

            return modelBuilder;
        }
    }
}