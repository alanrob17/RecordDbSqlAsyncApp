using DAL.Components;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ap = DAL.Components.AppSettings;

namespace DAL
{
    public class StatisticData
    {
        #region " Methods "

        public static async Task<Statistic> GetStatisticsAsync()
        {
            Statistic statistics = new();

            int? numCds = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Total number of CD's               
                var getValue = new SqlCommand("select sum(discs) from record where media = 'CD'", cn);
                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();

                if (result != DBNull.Value && result != null)
                {
                    numCds = (int?)result;
                }

                statistics.TotalCDs = numCds ?? 0;

                cn.Close();
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Rock Records
                int? rockDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Rock'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    rockDisks = (int?)result;
                }

                cn.Close();

                statistics.RockDisks = rockDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Folk Records
                int? folkDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Folk'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    folkDisks = (int?)result;
                }

                cn.Close();

                statistics.FolkDisks = folkDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Acoustic Records
                int? acousticDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Acoustic'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    acousticDisks = (int?)result;
                }

                cn.Close();

                statistics.AcousticDisks = acousticDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Jazz and Fusion Records
                int? jazzDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Jazz' or field = 'Fusion'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    jazzDisks = (int?)result;
                }

                cn.Close();
                statistics.JazzDisks = jazzDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Blues Records
                int? bluesDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Blues'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    bluesDisks = (int?)result;
                }

                cn.Close();
                statistics.BluesDisks = bluesDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Country Records
                int? countryDisks = null;

                var getValue = new SqlCommand("select sum(discs) from record where field = 'Country'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    countryDisks = (int?)result;
                }

                cn.Close();
                statistics.CountryDisks = countryDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Classical Records
                int? classicalDisks = null;

                var getValue = new SqlCommand("select sum(discs) from record where field = 'Classical'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    classicalDisks = (int?)result;
                }

                cn.Close();
                statistics.ClassicalDisks = classicalDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Soundtrack Records
                int? soundtrackDisks = null;
                var getValue = new SqlCommand("select sum(discs) from record where field = 'Soundtrack'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    soundtrackDisks = (int?)result;
                }

                cn.Close();
                statistics.SoundtrackDisks = soundtrackDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Four Star Records
                int? fourStarDisks = null;
                var getValue = new SqlCommand("select count(rating) from record where Rating = '****'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    fourStarDisks = (int?)result;
                }

                cn.Close();
                statistics.FourStarDisks = fourStarDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Three Star Records
                int? threeStarDisks = null;
                var getValue = new SqlCommand("select count(rating) from record where Rating = '***'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    threeStarDisks = (int?)result;
                }

                cn.Close();
                statistics.ThreeStarDisks = threeStarDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of Two Star Records
                int? twoStarDisks = null;
                var getValue = new SqlCommand("select count(rating) from record where Rating = '**'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    twoStarDisks = (int?)result;
                }

                cn.Close();
                statistics.TwoStarDisks = twoStarDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for number of One Star Records
                int? oneStarDisks = null;
                var getValue = new SqlCommand("select count(rating) from record where Rating = '*'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    oneStarDisks = (int?)result;
                }

                cn.Close();
                statistics.OneStarDisks = oneStarDisks ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on records
                decimal recordCost = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record where media = 'R'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    recordCost = (decimal)result;
                }

                cn.Close();
                statistics.RecordCost = recordCost;
            }

            var cdCost = 0.0m;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's               
                var getValue = new SqlCommand("select sum(cost) from record where media = 'CD'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cdCost = (decimal)result;
                }

                cn.Close();
                statistics.CDCost = cdCost;
            }

            // calculate the average cost of all CDs
            decimal avCdCost = cdCost / (decimal)numCds;
            statistics.AvCDCost = avCdCost;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on records and CD's
                var totalCost = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    totalCost = (decimal)result;
                }

                cn.Close();
                statistics.TotalCost = totalCost;
            }

            int? disks2017 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2017               
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2016' and bought < '01-Jan-2018'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2017 = (int?)result;
                }

                cn.Close();
                statistics.Disks2017 = disks2017 ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2017
                var cost2017 = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2016' and bought < '01-Jan-2018'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2017 = (decimal)result;
                }

                // this is to stop a divide by zero error if nothing has been bought
                if (cost2017 > 1)
                {
                    statistics.Cost2017 = cost2017;
                    var av2017 = cost2017 / (decimal)disks2017;
                    statistics.Av2017 = av2017;
                }
                else
                {
                    statistics.Cost2017 = 0.00m;
                    statistics.Av2017 = 0.00m;
                }

                cn.Close();
            }

            int? disks2018 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2018              
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2017' and bought < '01-Jan-2019'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2018 = (int?)result;
                }

                cn.Close();
                statistics.Disks2018 = disks2018 ?? 0;
            }

            var cost2018 = 0.0m;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2018                
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2017' and bought < '01-Jan-2019'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2018 = (decimal)result;
                }

                // this is to stop a divide by zero error if nothing has been bought
                if (cost2018 > 1)
                {
                    statistics.Cost2018 = cost2018;
                    var av2018 = cost2018 / (decimal)disks2018;
                    statistics.Av2018 = av2018;
                }
                else
                {
                    statistics.Cost2018 = 0.00m;
                    statistics.Av2018 = 0.00m;
                }

                cn.Close();
                statistics.Cost2018 = cost2018;
            }

            int? disks2019 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2019
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2018' and bought < '01-Jan-2020'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2019 = (int?)result;
                }

                cn.Close();
                statistics.Disks2019 = disks2019 ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2019
                var cost2019 = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2018' and bought < '01-Jan-2020'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2019 = (decimal)result;
                }

                // this is to stop a divide by zero error if nothing has been bought
                if (cost2019 > 1)
                {
                    statistics.Cost2019 = cost2019;
                    var av2019 = cost2019 / (decimal)disks2019;
                    statistics.Av2019 = av2019;
                }
                else
                {
                    statistics.Cost2019 = 0.00m;
                    statistics.Av2019 = 0.00m;
                }

                cn.Close();
            }

            int? disks2020 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2020
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2019' and bought < '01-Jan-2021'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2020 = (int?)result;
                }

                cn.Close();
                statistics.Disks2020 = disks2020 ?? 0;
            }

            var cost2020 = 0.0m;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2020
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2019' and bought < '01-Jan-2021'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2020 = (decimal)result;
                }

                var av2020 = cost2020 / (decimal)disks2020;
                statistics.Av2020 = av2020;

                cn.Close();
                statistics.Cost2020 = cost2020;
            }

            int? disks2021 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2021
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2020' and bought < '01-Jan-2022'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2021 = (int?)result;
                }

                cn.Close();
                statistics.Disks2021 = disks2021 ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2021
                var cost2021 = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2020' and bought < '01-Jan-2022'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2021 = (decimal)result;
                }

                // this is to stop a divide by zero error if nothing has been bought
                if (cost2021 > 1)
                {
                    statistics.Cost2021 = cost2021;
                    var av2021 = cost2021 / (decimal)disks2021;
                    statistics.Av2021 = av2021;
                }
                else
                {
                    statistics.Cost2021 = 0.00m;
                    statistics.Av2021 = 0.00m;
                }

                cn.Close();
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of records
                int? totalRecords = null;
                var getValue = new SqlCommand("select sum(discs) from record where media='R'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    totalRecords = (int?)result;
                }

                cn.Close();
                statistics.TotalRecords = totalRecords ?? 0;
            }

            int? disks2022 = null;
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for Number of CD's bought in 2022
                var getValue = new SqlCommand("select sum(discs) from record where bought > '31-Dec-2021' and bought < '01-Jan-2023'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    disks2022 = (int?)result;
                }

                cn.Close();
                statistics.Disks2022 = disks2022 ?? 0;
            }

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                // query for amount spent on CD's in 2022
                var cost2022 = 0.0m;
                var getValue = new SqlCommand("select sum(cost) from record where bought > '31-Dec-2021' and bought < '01-Jan-2023'", cn);

                await cn.OpenAsync();

                var result = await getValue.ExecuteScalarAsync();
                if (result != DBNull.Value)
                {
                    cost2022 = (decimal)result;
                }

                // this is to stop a divide by zero error if nothing has been bought
                if (cost2022 > 1)
                {
                    statistics.Cost2022 = cost2022;
                    var av2022 = cost2022 / (decimal)disks2022;
                    statistics.Av2022 = av2022;
                }
                else
                {
                    statistics.Cost2022 = 0.00m;
                    statistics.Av2022 = 0.00m;
                }

                cn.Close();
            }

            return statistics;
        }

        #endregion
    }
}
