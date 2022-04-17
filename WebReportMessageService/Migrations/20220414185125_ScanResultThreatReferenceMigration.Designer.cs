﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebReportMessageService;

namespace WebReportMessageService.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20220414185125_ScanResultThreatReferenceMigration")]
    partial class ScanResultThreatReferenceMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("WebReportMessageService.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("MessageType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebReportMessageService.NetworkResource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IpAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourceName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NetworkResources");
                });

            modelBuilder.Entity("WebReportMessageService.ScanJobResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PlanNextScan")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScanDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SuccessScanned")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ThreatId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalResources")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ScanJobResults");
                });

            modelBuilder.Entity("WebReportMessageService.ScanJobSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobRestartMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PingFailureThreat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PingRetries")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ScanJobSettings");
                });

            modelBuilder.Entity("WebReportMessageService.Threat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAppeared")
                        .HasColumnType("TEXT");

                    b.Property<string>("ThreatMessage")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Threats");
                });
#pragma warning restore 612, 618
        }
    }
}
