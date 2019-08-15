using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    enum WebSite
    {
        SanskritDocuments,
        WikiYoga,
        Ashtadhyayi,
        Empty
    }

    class Program
    {

        static void Main1(string[] args)
        {
            List<string> datas = new List<string>();

            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            //string[] links = new string[]
            //{
            //    "https://sanskritdocuments.org/doc_z_misc_major_works/dhatupatha_svara.html?lang=sa",
            //    "https://sanskritdocuments.org/doc_z_misc_major_works/dhatupatha_index_svara.html?lang=sa",
            //    "https://wiki.yoga-vidya.de/Sanskrit_Verbal_Roots_List_with_English_Translation",
            //    "http://ashtadhyayi.com/dhatu/"
            //};

            //foreach (string link in links)
            //{
            //    using (WebClient webClient = new WebClient())
            //    {
            //        WebSite webSite = WebSite.Empty;
            //        if (link.StartsWith("https://sanskritdocuments.org"))
            //        {
            //            webSite = WebSite.SanskritDocuments;
            //        }
            //        else if (link.StartsWith("https://wiki.yoga-vidya.de"))
            //        {
            //            webSite = WebSite.WikiYoga;
            //        }
            //        else if (link.StartsWith("http://ashtadhyayi.com/"))
            //        {
            //            webSite = WebSite.Ashtadhyayi;
            //        }
            //        //webClient.DownloadFile(link, path + "4.html");
            //        SetData(datas, webSite, Encoding.UTF8.GetString(webClient.DownloadData(link)));
            //    };
            //}

            foreach (string link in Directory.EnumerateFiles(path))
            {
                WebSite webSite = WebSite.Empty;
                if (link.StartsWith(path + "1") || link.StartsWith(path + "2"))
                {
                    webSite = WebSite.SanskritDocuments;
                }
                else if (link.StartsWith(path + "3"))
                {
                    webSite = WebSite.WikiYoga;
                }
                else if (link.StartsWith(path + "4"))
                {
                    webSite = WebSite.Ashtadhyayi;
                }
                //webClient.DownloadFile(link, path + "4.html");
                SetData(datas, webSite, File.ReadAllText(link));
            }

            File.WriteAllText(path + "result1.txt", datas[0]);
            File.WriteAllText(path + "result2.txt", datas[1]);
            File.WriteAllText(path + "result3.txt", datas[2]);
            File.WriteAllText(path + "result4.txt", datas[3]);
        }

        static void Main2(string[] args)
        {
            //List<string> datas = new List<string>();

            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            (List<List<string>> list, List<List<string>> missed) vs = (new List<List<string>>(), new List<List<string>>());

            foreach (string file in Directory.EnumerateFiles(path))
            {
                if (file.StartsWith(path + "result1"))
                {
                    GetList(vs, File.ReadAllLines(file).ToList(), @"^\d");
                }
                else if (file.StartsWith(path + "result2"))
                {
                    GetList(vs, File.ReadAllLines(file).ToList(), @"(\d\s॥)$");
                }
                else if (file.StartsWith(path + "result3"))
                {
                    GetList(vs, File.ReadAllLines(file).ToList(), @"[\u0900-\u097F]|[\uA8E0–\uA8FF]|[\u1CD0–\u1CFF]");
                }
                else if (file.StartsWith(path + "result4"))
                {
                    GetList(vs, File.ReadAllLines(file).ToList(), @"^\d");
                }
            }

            File.WriteAllLines(path + "subresult1.txt", vs.list[0]);
            File.WriteAllLines(path + "subresult2.txt", vs.list[1]);
            File.WriteAllLines(path + "subresult3.txt", vs.list[2]);
            File.WriteAllLines(path + "subresult4.txt", vs.list[3]);
        }

        static void Main3(string[] args)
        {
            Dictionary<string, string> numbers = new Dictionary<string, string>()
            {
                { "०", "0" },
                { "१", "1" },
                { "२", "2" },
                { "३", "3" },
                { "४", "4" },
                { "५", "5" },
                { "६", "6" },
                { "७", "7" },
                { "८", "8" },
                { "९", "9" }
            };

            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            //Dictionary<string, string> vs = new Dictionary<string, string>();

            (List<string> list1, List<string> list2, List<string> list3) vs = (new List<string>(), new List<string>(), new List<string>());

            foreach (string file in Directory.EnumerateFiles(path))
            {
                if (file.StartsWith(path + "subresult1"))
                {
                    Eliminate(vs.list1, File.ReadAllLines(file).ToList(), numbers);
                }
                else if (file.StartsWith(path + "subresult2"))
                {
                    Eliminate(vs.list2, File.ReadAllLines(file).ToList(), numbers);
                }
                else if (file.StartsWith(path + "subresult4"))
                {
                    Eliminate(vs.list3, File.ReadAllLines(file).ToList(), numbers);
                }
            }

            List<string> list1 = vs.list1.Except(vs.list3).ToList();
            List<string> list3 = vs.list3.Except(vs.list1).ToList();

            List<string> orderList1 = vs.list1.OrderBy(a => a).ToList();
            List<string> orderList2 = vs.list2.OrderBy(a => a).ToList();
            List<string> orderList3 = vs.list3.OrderBy(a => a).ToList();

            var fg = orderList1.GroupBy(a => a.Split('.')[0]).Select(b => b.ToList()).ToList();

            List<List<string>> final1 = orderList1.GroupBy(a => a.Split('.')[0]).Select(b => b.ToList()).ToList();
            List<List<string>> final2 = orderList2.GroupBy(a => a.Split('.')[0]).Select(b => b.ToList()).ToList();
            List<List<string>> final3 = orderList3.GroupBy(a => a.Split('.')[0]).Select(b => b.ToList()).ToList();

            List<string> result = list1.Union(list3).OrderBy(a => a).ToList();
            File.WriteAllLines(path + "allresult1.txt", final1.Select(a => a[0] + " - " + a[a.Count - 1]));
            File.WriteAllLines(path + "allresult2.txt", final2.Select(a => a[0] + " - " + a[a.Count - 1]));
            File.WriteAllLines(path + "allresult3.txt", final3.Select(a => a[0] + " - " + a[a.Count - 1]));
            File.WriteAllLines(path + "result.txt", result);
        }

        static List<Dhatus> R4()
        {

            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            List<Dhatus> dhatus = new List<Dhatus>();

            List<string> data = File.ReadAllLines(path + "result4.txt").ToList();
            data.RemoveAll(a => string.IsNullOrWhiteSpace(a));
            List<string> temp = data.Select(a => a.Trim()).ToList();

            for (int i = 0; i < temp.Count;)
            {
                int j = temp.FindIndex(i + 1, a => Regex.IsMatch(a, @"^\d"));
                if (j < i)
                {
                    j = temp.Count;
                }
                dhatus.Add(AddDhatus(i, j, temp));
                i = j;
            }

            return dhatus;
            //File.WriteAllLines(path + "settva.txt", dhatus.Select(a => a.Settva).Distinct());
            //File.WriteAllLines(path + "gana.txt", dhatus.Select(a => a.Gana).Distinct());
            //File.WriteAllLines(path + "pada.txt", dhatus.Select(a => a.Pada).Distinct());
            //File.WriteAllLines(path + "karma.txt", dhatus.Select(a => a.Karma).Distinct());

            //File.WriteAllLines(path + "subresult4.txt", vs.list[3]);
        }

        static List<Dhatus> R3()
        {
            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            List<Dhatus> dhatus = new List<Dhatus>();

            List<string> data = File.ReadAllLines(path + "subresult3.txt").ToList();
            data.RemoveAll(a => string.IsNullOrWhiteSpace(a));
            List<List<string>> temp = data.Select(a => a.Trim().Split(',').Skip(2).ToList()).ToList();
            List<string> allList = temp.Select(a => a?.FirstOrDefault()).ToList();

            List<Dhatus> dhatus1 = new List<Dhatus>();

            dhatus1.AddRange(allList.Select(a => new Dhatus() { Dhatu = a }));

            return dhatus1;
        }

        static List<Dhatus> R2()
        {
            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            List<Dhatus> dhatus = new List<Dhatus>();

            List<string> data = File.ReadAllLines(path + "subresult2.txt").ToList();

            //int set = data.FindAll(a => a.Contains("सेट्")).Count;
            //int anit = data.FindAll(a => a.Contains("अनिट्")).Count;
            //int vet = data.FindAll(a => a.Contains("वेट्")).Count;

            //int chu = data.FindAll(a => a.Contains("चु०")).Count;
            //int bva = data.FindAll(a => a.Contains("भ्वा०")).Count;
            //int ru = data.FindAll(a => a.Contains("रु०")).Count;
            //int di = data.FindAll(a => a.Contains("दि०")).Count;
            //int aa = data.FindAll(a => a.Contains("अ०")).Count;
            //int sva = data.FindAll(a => a.Contains("स्वा०")).Count;
            //int krya = data.FindAll(a => a.Contains("क्र्या०")).Count;
            //int tu = data.FindAll(a => a.Contains("तु०")).Count;
            //int ju = data.FindAll(a => a.Contains("जु०")).Count;
            //int ta = data.FindAll(a => a.Contains("त०")).Count;

            //int u = data.FindAll(a => a.Contains("उ०")).Count;
            //int A = data.FindAll(a => a.Contains("आ०")).Count;
            //int pa = data.FindAll(a => a.Contains("प०")).Count;

            data.RemoveAll(a => string.IsNullOrWhiteSpace(a));
            List<List<string>> temp = data.Select((a, b) =>
            {
                var ok = a.Trim().Split('।').ToList();
                if (ok.Count > 3)
                {
                    if ((ok[2].Contains("सेट्") || ok[2].Contains("अनिट्") || ok[2].Contains("वेट्")) && ok[2].Contains("०"))
                    {
                        var fg = ok.Take(3).ToList();
                        fg.RemoveAt(1);
                        return fg;
                    }
                    else if ((ok[1].Contains("सेट्") || ok[1].Contains("अनिट्") || ok[1].Contains("वेट्")) && ok[1].Contains("०"))
                    {
                        if (ok[2].Contains("उ०") || ok[2].Contains("आ०") || ok[2].Contains("प०"))
                        {
                            var hj = ok.Take(3).ToList();
                            return new List<string>()
                            {
                                hj[0],
                                hj[1]+hj[2]
                            };
                        }
                        return ok.Take(2).ToList();
                    }
                    else
                    {

                    }
                }
                return ok.Take(2).ToList();
            }).ToList();
            string[] oh = new string[] { " " };
            string[] no = new string[] { "(", ")" };
            var list = temp.Select((a, b) =>
              {
                  if (a.Count > 1)
                  {
                      Dhatus dhatus1 = new Dhatus();
                      string[] vs = null;
                      if (a[0].Contains("("))
                      {
                          vs = a[0].Split(no, StringSplitOptions.RemoveEmptyEntries);
                          dhatus1.Dhatu = vs[0];
                          dhatus1.Aupadeshik = vs[1].Replace(")", "");
                      }
                      else
                      {
                          dhatus1.Dhatu = a[0];
                      }
                      vs = a.Find(x => (x.Contains("सेट्") || x.Contains("अनिट्") || x.Contains("वेट्")) && x.Contains("०"))?.Split(oh, StringSplitOptions.RemoveEmptyEntries);

                      if (vs == null)
                      {
                          return dhatus1;
                      }
                      while (Regex.IsMatch(vs[vs.Length - 1], "\\d."))
                      {
                          var fghi = vs.ToList();
                          fghi.RemoveAt(vs.Length - 1);
                          vs = fghi.ToArray();
                      }
                  lab:
                      if (vs.Length == 4)
                      {
                          if (vs[2] == "वेट्" || vs[2] == "(अनिट्)" || vs[2] == "सेट्" || vs[2] == "अनिट्" || vs[2] == "सेट्०")
                          {
                              vs = new string[]
                              {
                              vs[0],
                              vs[1]+" / "+vs[2],
                              vs[3]
                              };
                          }
                          else
                          {
                              vs = new string[]
                              {
                              vs[0],
                              vs[1],
                              vs[2]+" / "+vs[3]
                              };
                          }
                      }
                      else if (vs.Length > 3 || vs[2] == "वेट्")
                      {
                          vs = new string[]
                          {
                              vs[0],
                              vs[1]+" / "+vs[2],
                              vs[5]
                          };
                          goto lab;
                      }

                      string tempGana = vs[0].Replace("०", "");

                      dhatus1.Gana = tempGana.Contains("/") ?
                      (dhatus1.Ganas[(int)(Ganashort)(Enum.Parse(typeof(Ganashort), tempGana.Split('/')[0], true))] + " / " + (dhatus1.Ganas[(int)(Ganashort)(Enum.Parse(typeof(Ganashort), tempGana.Split('/')[1], true))])) : (dhatus1.Ganas[(int)(Ganashort)(Enum.Parse(typeof(Ganashort), tempGana, true))]);

                      dhatus1.Settva = vs[1].Replace("०", "").Replace("(", "").Replace(")", "");

                      string tempPada = vs[2].Replace("०", "");

                      dhatus1.Pada = tempPada.Contains("/") ?
                      (dhatus1.Padas[(int)(Padashort)(Enum.Parse(typeof(Padashort), tempPada.Split('/')[0], true))] + " / " + (dhatus1.Padas[(int)(Padashort)(Enum.Parse(typeof(Padashort), tempPada.Split('/')[1], true))])) : (dhatus1.Padas[(int)(Padashort)(Enum.Parse(typeof(Padashort), tempPada, true))]);
                      //dhatus1.Padas[(int)(Padashort)(Enum.Parse(typeof(Padashort), vs[2].Replace("०", "")))];
                      return dhatus1;
                  }
                  return null;
              }).ToList();
            list.RemoveAll(a => a == null);

            File.WriteAllLines(path + "tempGana.txt", list.Select(a => a.Gana).Distinct());
            File.WriteAllLines(path + "tempSettva.txt", list.Select(a => a.Settva).Distinct());
            File.WriteAllLines(path + "tempPada.txt", list.Select(a => a.Pada).Distinct());

            //int set1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Settva) && a.Settva.Contains("सेट्")).Count;
            //int anit1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Settva) && a.Settva.Contains("अनिट्")).Count;
            //int vet1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Settva) && a.Settva.Contains("वेट्")).Count;

            //int chu1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("चु०")).Count;
            //int bva1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("भ्वा०")).Count;
            //int ru1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("रु०")).Count;
            //int di1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("दि०")).Count;
            //int aa1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("अ०")).Count;
            //int sva1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("स्वा०")).Count;
            //int krya1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("क्र्या०")).Count;
            //int tu1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("तु०")).Count;
            //int ju1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("जु०")).Count;
            //int ta1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Gana) && a.Gana.Contains("त०")).Count;

            //int u1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("उ०")).Count;
            //int A1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("आ०")).Count;
            //int pa1 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("प०")).Count;

            //var u2 = data.FindAll(a => a.Contains("उ०")).Select((z, x) => x).ToList();
            //var A2 = data.FindAll(a => a.Contains("आ०")).Select((z, x) => x).ToList();
            //var pa2 = data.FindAll(a => a.Contains("प०")).Select((z, x) => x).ToList();

            //var u3 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("उ०")).Select((z, x) => x).ToList();
            //var A3 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("आ०")).Select((z, x) => x).ToList();
            //var pa3 = list.FindAll(a => !string.IsNullOrWhiteSpace(a?.Pada) && a.Pada.Contains("प०")).Select((z, x) => x).ToList();

            //var ms1 = u2.Except(u3).ToList();
            //var ms2 = A2.Except(A3).ToList();
            //var ms3 = pa2.Except(pa3).ToList();

            return list;
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\ksdma\OneDrive\Desktop\";

            List<Dhatus> dhatus = R2().OrderBy(a => a.Dhatu).ToList();
            List<Dhatus> dhatus1 = R3().OrderBy(a => a.Dhatu).ToList();
            List<Dhatus> dhatus2 = R4().OrderBy(a => a.Dhatu).ToList();

            File.WriteAllLines(path + "done.txt", dhatus.Select(a => a.Dhatu + "\t" + a.Aupadeshik + "\t" + a.Gana + "\t" + a.Pada + "\t" + a.Settva + "\t" + a.Karma));
            File.WriteAllLines(path + "done1.txt", dhatus1.Select(a => a.Dhatu + "\t" + a.Aupadeshik + "\t" + a.Gana + "\t" + a.Pada + "\t" + a.Settva + "\t" + a.Karma));
            File.WriteAllLines(path + "done2.txt", dhatus2.Select(a => a.Dhatu + "\t" + a.Aupadeshik + "\t" + a.Gana + "\t" + a.Pada + "\t" + a.Settva + "\t" + a.Karma));
        }

        private static Dhatus AddDhatus(int i, int j, List<string> temp)
        {
            Dhatus dhatus = new Dhatus();
            while (j - i > 8)
            {
                dhatus.Tag += temp[--j];
            }
            dhatus.Karma = temp[--j];
            dhatus.Settva = temp[--j];
            dhatus.Pada = temp[--j];
            dhatus.Gana = temp[--j];
            dhatus.Artha = temp[--j];
            lab:
            dhatus.Aupadeshik = temp[--j];
            dhatus.Dhatu = temp[--j];
            dhatus.Number = temp[--j];
            if(dhatus.Dhatu== "01.1166")
            {
                j += 4;
                dhatus.Artha = "";
                goto lab;
            }
            return dhatus;
        }

        private static void Eliminate(List<string> list, List<string> data, Dictionary<string, string> numbers)
        {
            if (data.All(a => Regex.IsMatch(a, @"(\d\s॥)$")))
            {
                list.AddRange(data.Select(a =>
                {
                    string[] sp = a.Split(' ');
                    string str = sp[sp.Length - 2];
                    foreach (KeyValuePair<string, string> item in numbers)
                    {
                        str = str.Replace(item.Key, item.Value);
                    }
                    string[] vs = str.Split('.');

                    if (vs[1].Length == 1)
                    {
                        str = vs[0] + ".000" + vs[1];
                    }
                    else if (vs[1].Length == 2)
                    {
                        str = vs[0] + ".00" + vs[1];
                    }
                    else if (vs[1].Length == 3)
                    {
                        str = vs[0] + ".0" + vs[1];
                    }

                    if (vs[0].Length == 1)
                    {
                        str = "0" + str;
                    }

                    return str;
                }));
            }
            else if (data.All(a => a.Contains(" ")))
            {
                list.AddRange(data.Select(a =>
                {
                    string str = a.Split(' ')[0];
                    foreach (KeyValuePair<string, string> item in numbers)
                    {
                        str = str.Replace(item.Key, item.Value);
                    }
                    string[] vs = str.Split('.');

                    if (vs[1].Length == 1)
                    {
                        str = vs[0] + ".000" + vs[1];
                    }
                    else if (vs[1].Length == 2)
                    {
                        str = vs[0] + ".00" + vs[1];
                    }
                    else if (vs[1].Length == 3)
                    {
                        str = vs[0] + ".0" + vs[1];
                    }

                    if (vs[0].Length == 1)
                    {
                        str = "0" + str;
                    }

                    return str;
                }));
            }
            else
            {
                list.AddRange(data);
            }
        }

        private static void GetList((List<List<string>> list, List<List<string>> missed) datas, List<string> data, string pattern)
        {
            data.RemoveAll(a => string.IsNullOrWhiteSpace(a));
            List<string> temp = data.Select(a => a.Trim()).ToList();
            var list = temp.Where(a => Regex.IsMatch(a, pattern)).ToList();
            var missed = temp.Except(list).ToList();
            datas.list.Add(list);
            datas.missed.Add(missed);
        }

        private static void SetData(List<string> datas, WebSite webSite, string data)
        {
            string temp = null;
            switch (webSite)
            {
                case WebSite.SanskritDocuments:
                    {
                        temp = GetString(data, "id=\"content\"");
                        break;
                    }
                case WebSite.WikiYoga:
                    {
                        temp = GetString(data, "id=\"mw-content-text\"");
                        break;
                    }
                case WebSite.Ashtadhyayi:
                    {
                        temp = GetString(data, "id=\"dhatu-list-table\"");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (temp != null)
            {
                datas.Add(temp);
            }
        }

        private static string GetString(string input, string attr)
        {
            string temp = "";
            string pattern = @"<(\w+)\s[^>]*" + attr + @"[^>]*>[\s\S]*\1";
            //@"<(\w+)\s[^>]*" + attr + @"[^>]*>[\s\S]*\1>";
            MatchCollection matchCollection = Regex.Matches(input, pattern);

            if (matchCollection.Count > 1)
            {
                Debugger.Break();
            }
            else
            {
                temp = matchCollection.OfType<Match>().SingleOrDefault().Value + ">";
            }

            temp = ReplaceString(temp, "<.*?(>)");
            temp = ReplaceString(temp, "\t{1,}");
            temp = ReplaceString(temp, "\n{1,}|\r{1,}", "\r\n");
            temp = temp.Replace("&nbsp;", "");

            return temp;
        }

        private static string ReplaceString(string input, string pattern, string repl = "\t")
        {
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return Regex.Replace(input, pattern, repl, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
    }

    enum Ganashort
    {
        भ्वा,
        अ,
        जु,
        दि,
        स्वा,
        तु,
        रु,
        त,
        क्र्या,
        चु
    }

    enum Padashort
    {
        प,
        आ,
        उ
    }

    class Dhatus
    {
        public string Number { get; set; }
        public string Dhatu { get; set; }
        public string Aupadeshik { get; set; }
        public string Artha { get; set; }

        public string Gana { get; set; }
        public string Pada { get; set; }
        public string Settva { get; set; }
        public string Karma { get; set; }
        public string Tag { get; set; }

        //List<string> Ganashort = new List<string>()
        //{
        //    "भ्वा",
        //    "अ",
        //    "जु",
        //    "दि",
        //    "स्वा",
        //    "तु",
        //    "रु",
        //    "त",
        //    "क्र्या",
        //    "चु"
        //};

        //List<string> Padashort = new List<string>()
        //{
        //    "प",
        //    "आ",
        //    "उ"
        //};

        public List<string> Ganas = new List<string>()
        {
            "भ्वादि:",
            "अदादि:",
            "जुहोत्यादि:",
            "दिवादि:",
            "स्वादि:",
            "तुदादि:",
            "रुधादि:",
            "तनादि:",
            "क्र्यादि:",
            "चुरादि:"
        };

        public List<string> Settvas = new List<string>()
        {
            "सेट्",
            "वेट्",
            "अनिट्",
            "अनिट् / सेट्"
        };

        public List<string> Padas = new List<string>()
        {
            "परस्मैपदी",
            "आत्मनेपदी",
            "आत्मनेपदी / परस्मैपदी",
            "उभयपदी"
        };

        public List<string> Karmas = new List<string>()
        {
            "अकर्मकः",
            "अकर्मकः / सकर्मकः",
            "सकर्मकः",
            "सकर्मकः / द्विकर्मकः",
            "सकर्मकः / अकर्मकः",
            "द्विकर्मकः"
        };
    }
}
