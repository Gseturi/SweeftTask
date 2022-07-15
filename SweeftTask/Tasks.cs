using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using System.Net;
using Newtonsoft.Json;
namespace SweeftTask
{
    internal class Tasks
    {
   
        
        public static bool sPalindrome(string s)
        {
            string x = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string y, t;

            int Length = (s.Length / 2);
            s = s.ToUpper();

            int mod = 0, mod2 = 0;

            for (int i = 0; i < Length; i++)
            {
                if (x.Contains(s[i + mod]) && x.Contains(s[(s.Length - 1) - i - mod2]))
                {

                    if (s[i + mod] != s[(s.Length - 1) - i - mod2])
                    {
                        return false;
                    }
                }
                else if (x.Contains(s[i + mod]) == false && x.Contains(s[(s.Length - 1) - i - mod2]) == false)
                {


                }
                else if (x.Contains(s[i + mod]) == true && x.Contains(s[(s.Length - 1) - i - mod2]) == false)
                {
                    i = i - 1;
                    mod2 = mod2 + 1;

                }
                else if (x.Contains(s[i + mod]) == false && x.Contains(s[(s.Length - 1) - i - mod2]) == true)
                {
                    i = i - 1;
                    mod = mod + 1;
                }


            }


            return true;
        }

        public static int MinSplit(int amount)
        {
            int x=0;
            int dev = 50;
            int temp = 0;
            while (amount!=0)
            {


                temp = amount / dev;
                amount = amount - temp * dev;
                x = x + temp;



                if (dev==50)
                {
                    dev = 20;
                }else if (dev==5)
                {
                    dev = 1;
                }
                else
                {
                    dev = dev / 2;
                }

                


             
            }

            return x;
        }


        public static int NotContains(int[] array)
        {
            Array.Sort(array);
            if (array[0] != 1)
            {
                return 1;
            }
            for(int i=1;i< array.Length; i++)
            {
                if (array[i]!=array[i-1]+1)
                {
                    return array[i - 1] + 1;
                }
               
            }
            return array[array.Length-1] + 1;
        }
        public static bool IsProperly(string x)
        {
            StringBuilder temp=new StringBuilder(x);

                for(int i = 0; i < x.Length; i++) { 
            
                if (x[i]!=')' && x[i]!='(')
                {
                    temp[i] = ' ';
                }
                
                }
               x=temp.ToString();
               x=x.Replace(" ",string.Empty);

            if (x.Length%2!=0)
            {
                return false;
            }


            for (int i = 0; i < x.Length / 2; i++)
            {
                if (x[i] !='(' || x[x.Length-1-i]!=')')
                {
                    return false;
                }

            }
            return true;
        }


        public static int CountVariants(int x)
        {

            if (x == 1 || x == 0)
            {
                return 1;
            }

            int[] temp = new int[x];
            temp[0] = 1;
            temp[1] = 1;
            for (int i = 2; i < x; i++)
            {
                temp[i] = temp[i - 1] + temp[i - 2];
            }
            return temp[x - 1] + temp[x - 2];
        }
        public static int CountVariants2(int x)
        {

            if (x == 1 || x == 0)
            {
                return 1;
            }


            int one = 1;
            int two = 2;
            int three = 0;
            for (int i = 2; i < x; i++)
            {
                three = one + two;
                one = two;
                two = three;
            }
            return three;
        }

    }
    public static class SQL {

        static private SqlConnection Con;
        static private SqlCommand Com;
        static private SqlDataAdapter Adapter;
        static private DataTable Table;
        static string ConnectionString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;

        static public DataTable GetAllTeachersByStudent(string studentName)
        {
            Con = new SqlConnection(ConnectionString);
            Com = new SqlCommand("ReturnTeacher", Con);
            Com.CommandType = CommandType.StoredProcedure;
             SqlParameter par = new SqlParameter();
            par = new SqlParameter("@name", SqlDbType.VarChar);
            par.Value = studentName;
            Com.Parameters.Add(par);

            Table=new DataTable();
            Adapter = new SqlDataAdapter(Com);

            Con.Open();
            Com.ExecuteNonQuery();
            Adapter.Fill(Table);
            Con.Close();
            //DataTable VisuaLizer
            return Table;
        }





    }

    public static class Web {


      
        public static void GenerateCountryDataFiles()
        {
            string path = @"C:\Users\User\source\repos\SweeftTask\SweeftTask\Countries\";

            string FileName;
            string Region;
            String Subregion;
            string Latlng;
            string Area;
            string Population;

            WebClient webClient = new WebClient();
            string Page = webClient.DownloadString("https://restcountries.com/v3.1/all");
            dynamic Dpage = JsonConvert.DeserializeObject<dynamic>(Page);


            int count = 0;
            List<string> temp;

            foreach (var d in Dpage)
            {
                path = @"C:\Users\User\source\repos\SweeftTask\SweeftTask\Countries\";

                string fileName = d.ToString();
                FileName = d["name"]["official"].ToString();
                Region = d["region"].ToString();
                Subregion = d["subregion"];
                Latlng = d["latlng"][0] + " " + d["latlng"][1];
                Area = d["area"];
                Population = d["population"];
                path = path + "" + FileName;

                if (File.Exists(path))
                {
                    File.Delete(path);
                }


                temp = new List<string>();

                temp.Add(Region);
                temp.Add(Latlng);
                temp.Add(Area);
                temp.Add(Population);


                 File.WriteAllLinesAsync(path, temp);



                path = " ";

            }

        }


    }

    


}
