using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfiumViewer;
using System.Drawing.Printing;

namespace PrinterServerApp
{
    internal static class Program
    {
        //CONFIG OBJECT
        private static IConfigurationRoot configuration;

        //HTTP OBJECT
        public static HttpClient Client { get; set; }

        //ERROR DISPLAY
        public static bool ERROR { get; set; } = false;
        public static string ERROR_MSG { get; set; }
        public static string ERROR_DETAIL { get; set; }

        //VIEW OBJECTS
        public static DataGridView DataGrid { get; set; }
        public static Label Status { get; set; }
        public static Label ErrMsg { get; set; }

        //END PARAMETER
        //IF TRUE STOPS PROGRAM
        public static bool EndProgram { get; set; } = false;

        //LOG PATH
        public static string LOG_PATH { get; set; }
        public static string ERROR_LOG { get; set; }
        public static string HTTP_ERROR_LOG { get; set; }
        public static string PRINT_ERROR_LOG { get; set; }

        //API ENDPOINTS
        public static string PRINT_LIST_API { get; set; }
        public static string RECEIPT_PDF_API { get; set; }
        public static string LABEL_PDF_API { get; set; }
        public static string SHUKA_IRAI_PDF_API { get; set; }
        public static string NOUHIN_PDF_API { get; set; }
        public static string DENPYO_PDF_API { get; set; }
        public static string ORDER_UPDATE_API { get; set; }
        public static string PRINT_HISTORY_API { get; set; }
        public static int API_RETRY { get; set; } = 3;

        //PDF PRINT FLAGS
        public static bool RECEIPT_PDF_PRINT { get; set; } = true;
        public static bool LABEL_PDF_PRINT { get; set; } = true;
        public static bool SHUKA_IRAI_PDF_PRINT { get; set; } = true;
        public static bool NOUHIN_PDF_PRINT { get; set; } = true;
        public static bool DENPYO_PDF_PRINT { get; set; } = true;


        //TEMP OUTPUT PATH
        public static string FILE_PATH { get; set; }

        //SLEEP・DELAY TIME
        // in miliseconds
        public static int SLEEP_TIME { get; set; } = 0;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //INITIALIZE CONFIGURATION SETTINGS
            SetConfig();

            //IF NO CONFIG INFO DISPLAY ERROR AND STOP PROGRAM
            if (ConfigErrorCheck())
            {
                MessageBox.Show("設定情報が見つかりませんでした。\nプログラムを終了します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //二重起動チェック
            // Double start check
            using Mutex mutex = new(true, "PrinterServer", out bool isNewInstance);

            if (!isNewInstance)
            {
                string logMessage = string.Format("[{0}] このプログラムは既に実行されています。\r\n", DateTime.Now.ToString());
                File.AppendAllText(LOG_PATH + ERROR_LOG, logMessage);
                MessageBox.Show("自動帳票発行システムは既に実行されています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //自動帳票発行
            AutoPrint();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }

        /// <summary>
        /// GET CONFIG DATA FROM FILE AND SET TO CONFIGURATION VARIABLES
        /// </summary>
        static void SetConfig()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddXmlFile("PrinterServerApp.dll.config")
                .Build();

            //LOG PATH
            LOG_PATH = configuration["section:key:LOG_PATH"];
            ERROR_LOG = configuration["section:key:ERROR_LOG"];
            HTTP_ERROR_LOG = configuration["section:key:HTTP_ERROR_LOG"];
            PRINT_ERROR_LOG = configuration["section:key:PRINT_ERROR_LOG"];

            //API ENDPOINTS
            PRINT_LIST_API = configuration["section:key:PRINT_LIST_API"];
            RECEIPT_PDF_API = configuration["section:key:RECEIPT_PDF_API"];
            LABEL_PDF_API = configuration["section:key:LABEL_PDF_API"];
            SHUKA_IRAI_PDF_API = configuration["section:key:SHUKA_IRAI_PDF_API"];
            NOUHIN_PDF_API = configuration["section:key:NOUHIN_PDF_API"];
            DENPYO_PDF_API = configuration["section:key:DENPYO_PDF_API"];
            ORDER_UPDATE_API = configuration["section:key:ORDER_UPDATE_API"];
            PRINT_HISTORY_API = configuration["section:key:PRINT_HISTORY_API"];

            //PDF PRINT FLAGS
            if (bool.TryParse(configuration["section:key:RECEIPT_PDF_PRINT"], out bool _receiptflg))
            {
                RECEIPT_PDF_PRINT = _receiptflg;
            }
            if (bool.TryParse(configuration["section:key:LABEL_PDF_PRINT"], out bool _labelflg))
            {
                LABEL_PDF_PRINT = _labelflg;
            }
            if (bool.TryParse(configuration["section:key:SHUKA_IRAI_PDF_PRINT"], out bool _shukaflg))
            {
                SHUKA_IRAI_PDF_PRINT = _shukaflg;
            }
            if (bool.TryParse(configuration["section:key:NOUHIN_PDF_PRINT"], out bool _nouhinflg))
            {
                NOUHIN_PDF_PRINT = _nouhinflg;
            }
            if (bool.TryParse(configuration["section:key:DENPYO_PDF_PRINT"], out bool _denpyoflg))
            {
                DENPYO_PDF_PRINT = _denpyoflg;
            }

            //TEMP OUTPUT PATH
            FILE_PATH = configuration["section:key:FILE_PATH"];

            //SLEEP TIME
            if (int.TryParse(configuration["section:key:SLEEP_TIME"], out int _time))
            {
                SLEEP_TIME = _time;
            }

            //API RETRY 
            if (int.TryParse(configuration["section:key:API_RETRY"], out int _retry))
            {
                API_RETRY = _retry;
            }

        }

        /// <summary>
        /// CHECK IF CONFIG DATA IS VALID
        /// <br> > string values are not null or empty</br>
        /// <br> > int values are not null or less than 0</br>
        /// </summary>
        /// <returns>True if data is invalid</returns>
        static bool ConfigErrorCheck()
        {
            if (String.IsNullOrEmpty(LOG_PATH))
            {
                return true;
            }

            if (String.IsNullOrEmpty(ERROR_LOG))
            {
                return true;
            }

            if (String.IsNullOrEmpty(HTTP_ERROR_LOG))
            {
                return true;
            }

            if (String.IsNullOrEmpty(PRINT_ERROR_LOG))
            {
                return true;
            }

            if (String.IsNullOrEmpty(PRINT_LIST_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(RECEIPT_PDF_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(LABEL_PDF_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(SHUKA_IRAI_PDF_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(NOUHIN_PDF_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(DENPYO_PDF_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(ORDER_UPDATE_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(PRINT_HISTORY_API))
            {
                return true;
            }

            if (String.IsNullOrEmpty(FILE_PATH))
            {
                return true;
            }

            if (SLEEP_TIME < 0)
            {
                return true;
            }

            if (API_RETRY < 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 自動帳票発行処理
        /// </summary>
        /// <returns></returns>
        static async Task AutoPrint()
        {

            Client = new HttpClient();

            while (true)
            {
                //Argument is true then stop program
                if (EndProgram)
                {
                    Environment.Exit(0);
                }

                //sleep
                await Task.Delay(SLEEP_TIME);

                //Get print list from server
                List<PrintList> printList = await GetPrintList(PRINT_LIST_API);

                //If error
                if (ERROR)
                {
                    break;
                }

                //If no print list skip
                if (printList == null || printList.Count < 1)
                {
                    //skip
                    continue;
                }

                //Update table view
                DisplayPrintHistory();

                //Loop through list
                foreach (var item in printList)
                {
                    try
                    {
                        //Argument is true then stop program
                        if (EndProgram)
                        {
                            Environment.Exit(0);
                        }

                        int pCnt = 0;

                        if (DENPYO_PDF_PRINT && item.Denpyo_flg == "1")

                        {
                            //get denpyo/print
                            if (await GetPdf(DENPYO_PDF_API, item.Order_no, "売上伝票", "A4"))

                            {
                                pCnt++;
                            }
                            else
                            {
                                break;
                            }
                        };

                        if (SHUKA_IRAI_PDF_PRINT && item.Hikae_flg == "1")

                        {
                            //get hikae
                            if (await GetPdf(SHUKA_IRAI_PDF_API, item.Order_no, "出荷依頼書", "A4"))

                            {
                                pCnt++;
                            }
                            else
                            {
                                break;
                            }
                        };

                        if (RECEIPT_PDF_PRINT && item.Receipt_flg == "1" && item.Order_flg == "0")

                        {
                            // get reciept/print
                            if (await GetPdf(RECEIPT_PDF_API, item.Order_no, "領収書", "A5"))

                            {
                                pCnt++;
                            }
                            else
                            {
                                break;
                            }
                        };

                        if (NOUHIN_PDF_PRINT && item.Order_flg == "1")

                        {
                            //get order form
                            if (await GetPdf(NOUHIN_PDF_API, item.Order_no, "納品書・注文書", "A3"))
                            {
                                pCnt++;
                            }
                            else
                            {
                                break;
                            }
                        };

                        if (LABEL_PDF_PRINT && item.Label_flg == "1")
                        {
                            //get label
                            if (await GetPdf(LABEL_PDF_API, item.Order_no, "荷札・送り状", ""))
                            {
                                pCnt++;
                            }
                            else
                            {
                                break;
                            }
                        };

                        //UPDATE print flg
                        if (pCnt != 0)
                        {

                            if (!await UpdatePrintFlg(ORDER_UPDATE_API, item.Order_no))
                            {
                                break;
                            }

                            DisplayPrintHistory();
                        }

                    }
                    catch (Exception ex)
                    {
                        string logMessage = string.Format("[{0}] [受注No.{1}] 処理中にエラーが発生しました： {2}\r\n", DateTime.Now.ToString(), item.Order_no, ex.Message);
                        File.AppendAllText(LOG_PATH + ERROR_LOG, logMessage);
                    }
                }

                if (ERROR)
                {
                    break;
                }
            }

        }

        /// <summary>
        /// Get list of orders to be printed
        /// </summary>
        /// <param name="url">The Api Endpoint</param>
        /// <returns>Print list</returns>
        static async Task<List<PrintList>> GetPrintList(string url)
        {
            int tryCnt = 0;
            while (tryCnt < API_RETRY)
            {
                try
                {
                    var response = await Client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PrintList>>(jsonResult);
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during http request
                    string logMessage = string.Format("[{0}] 印刷リストの取得中にエラーが発生しました： {1}\r\n", DateTime.Now.ToString(), ex.Message);
                    File.AppendAllText(LOG_PATH + HTTP_ERROR_LOG, logMessage);
                    tryCnt++;
                    ERROR_DETAIL = ex.Message;
                }
            }

            ERROR_MSG = "印刷リストの取得中にネットワークエラーが発生しました。";
            ERROR = true;
            DisplayError();
            return null;
        }

        /// <summary>
        /// SET PRINT HISTORY TABLE DATASOURCE
        /// </summary>
        public async static void DisplayPrintHistory()
        {
            try
            {
                List<PrintHistory> printHistory = await GetPrintHistory();

                // Access the dataGrid control from the UI thread
                DataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    DataGrid.DataSource = printHistory;
                }));
            }
            catch (Exception ex)
            {
                string logMessage = string.Format("[{0}] 一覧の更新中にエラーが発生しました： {1}\r\n", DateTime.Now.ToString(), ex.Message);
                File.AppendAllText(LOG_PATH + ERROR_LOG, logMessage);
            }
        }

        /// <summary>
        /// GET PRINT HISTORY
        /// </summary>
        /// <returns>List of print history</returns>
        async static Task<List<PrintHistory>> GetPrintHistory()
        {
            int tryCnt = 0;
            while (tryCnt < API_RETRY)
            {
                try
                {
                    var response = await Client.GetAsync(PRINT_HISTORY_API);
                    response.EnsureSuccessStatusCode();

                    string jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PrintHistory>>(jsonResult);
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during http request
                    string logMessage = string.Format("[{0}] 印刷履歴の取得中にエラーが発生しました： {1}\r\n", DateTime.Now.ToString(), ex.Message);
                    File.AppendAllText(LOG_PATH + HTTP_ERROR_LOG, logMessage);
                    tryCnt++;
                }
            }
            return null;
        }

        /// <summary>
        /// UPDATE PRINT FLAG OF ORDER
        /// </summary>
        /// <param name="client">The HttpClien</param>
        /// <param name="url">The Api Endpoint</param>
        /// <param name="order_no">The order no, used if error</param>
        static async Task<bool> UpdatePrintFlg(string url, string order_no)
        {
            int tryCnt = 0;
            while (tryCnt < API_RETRY)
            {
                try
                {
                    var response = await Client.GetAsync(url + order_no);
                    response.EnsureSuccessStatusCode();
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonResult);

                    if (jsonObject.ContainsKey("error")) throw new Exception(jsonObject["error"].ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during http request
                    string logMessage = string.Format("[{0}] 受注No.{1}の更新中にエラーが発生しました： {2}\r\n", DateTime.Now.ToString(), order_no, ex.Message);
                    File.AppendAllText(LOG_PATH + HTTP_ERROR_LOG, logMessage);
                    tryCnt++;
                    ERROR_MSG = string.Format("[受注No.{0}]の更新中にエラーが発生しました。", order_no);
                    ERROR_DETAIL = ex.Message;
                }
            }
            ERROR = true;
            DisplayError();
            return false;
        }

        /// <summary>
        /// Get PDF from server and print out
        /// </summary>
        /// <param name="url">The Api Endpoint</param>
        /// <param name="report">The report name</param>
        /// <param name="pagesize">The pagesize of the pdf, used for printing.("A4", "A3")</param>
        /// <returns>True on success</returns>
        static async Task<bool> GetPdf(string url, string order_no, string report, string pagesize)
        {
            int tryCnt = 0;
            retryLoop:
            while (tryCnt < API_RETRY)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string formatDate = now.ToString("yyyyMMddHHmmssfff");

                    var response = await Client.GetAsync(url + order_no);
                    response.EnsureSuccessStatusCode();
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    List<PdfFile> pdfFiles = JsonConvert.DeserializeObject<List<PdfFile>>(jsonResult);

                    foreach (PdfFile pdfFile in pdfFiles)
                    {
                        //If Error
                        if (!string.IsNullOrEmpty(pdfFile.Error))
                        {
                            if (pdfFile.Error == "NG")
                            {
                                return true;
                            }
                            else
                            {
                                throw new Exception(pdfFile.Error);
                            }

                        }

                        //Check printer name
                        if (string.IsNullOrEmpty(pdfFile.Printer_nm))
                        {
                            throw new Exception("プリンタドライバを取得出来ませんでした。");
                        }

                        // Check if the 'pdf' property in the JSON is an empty string or null
                        if (pdfFile.Pdf == null || pdfFile.Pdf.Length == 0)
                        {
                            throw new Exception("PDFバイトを取得できませんでした。");
                        }

                        //filename
                        string file = string.Format("{0}{1}_{2}.pdf", FILE_PATH, DateTime.Now.ToString("yyyyMMddHHmmss"), report);

                        // Create and Save the PDF file
                        try
                        {
                            // Write the byte array to a file
                            File.WriteAllBytes(file, pdfFile.Pdf);
                        }
                        catch (Exception ex)
                        {
                            // Log any exceptions that occur during pdf creation
                            string logMessage = string.Format("[{0}] [受注No.{1}] {2}の構築中にエラーが発生しました： {3}\r\n", DateTime.Now.ToString(), order_no, report, ex.Message);
                            ERROR_MSG = string.Format("[受注No.{0}] {1}の構築中にエラーが発生しました。", order_no, report);
                            ERROR_DETAIL = ex.Message;
                            File.AppendAllText(LOG_PATH + ERROR_LOG, logMessage);
                            tryCnt++;
                            goto retryLoop;
                        }

                        //print file
                        try
                        {
                            PrintPdf(pdfFile.Printer_nm, file, pagesize, "");
                        }
                        catch (Exception ex)
                        {
                            // Log any exceptions that occur during printing
                            string logMessage = string.Format("[{0}] [受注No.{1}] {2}の印刷中にエラーが発生しました： {3}\r\n", DateTime.Now.ToString(), order_no, report, ex.Message);
                            File.AppendAllText(LOG_PATH + PRINT_ERROR_LOG, logMessage);
                            File.Delete(file);
                            tryCnt++;
                            ERROR_MSG = string.Format("[受注No.{0}] {1}の印刷中にエラーが発生しました。", order_no, report);
                            ERROR_DETAIL = ex.Message;
                            goto retryLoop;
                        }
                        finally
                        {
                           File.Delete(file);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during http request
                    string logMessage = string.Format("[{0}] [受注No.{1}] {2}の取得中にエラーが発生しました： {3}\r\n", DateTime.Now.ToString(), order_no, report, ex.Message);
                    File.AppendAllText(LOG_PATH + HTTP_ERROR_LOG, logMessage);
                    tryCnt++;
                    ERROR_MSG = string.Format("[受注No.{0}] {1}の取得中にエラーが発生しました。", order_no, report);
                    ERROR_DETAIL = ex.Message;
                    goto retryLoop;
                }
            }
            ERROR = true;
            DisplayError();
            return false;
        }

        /// <summary>
        /// PRINT OUT PDF
        /// </summary>
        /// <param name="printer">The printer name</param>
        /// <param name="file">The full filepath of file to be printed</param>
        /// <param name="paperSize">The size of the paper eg, A4</param>
        /// <param name="paperSource">The paper tray</param>
        static void PrintPdf(string printer, string file, string paperSize, string paperSource)
        {
            try
            {
                using PdfDocument pdfdoc = PdfDocument.Load(file);
                using PrintDocument pd = pdfdoc.CreatePrintDocument();

                pd.PrinterSettings.PrinterName = printer;

                // Set default paper size
                if (!string.IsNullOrEmpty(paperSize))
                {
                    for (int index = 0; index < pd.PrinterSettings.PaperSizes.Count; index++)
                    {
                        if (pd.PrinterSettings.PaperSizes[index].PaperName.Contains(paperSize) == true)
                        {
                            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes[index];
                            break;
                        }
                    }
                }

                // Set default paper source
                if (!string.IsNullOrEmpty(paperSource))
                {
                    for (int index = 0; index < pd.PrinterSettings.PaperSources.Count; index++)
                    {
                        if (pd.PrinterSettings.PaperSources[index].SourceName.Contains(paperSource) == true)
                        {
                            pd.DefaultPageSettings.PaperSource = pd.PrinterSettings.PaperSources[index];
                            break;
                        }
                    }
                }

                // Print the document
                pd.Print();
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during printing
                //string logMessage = string.Format("[{0}] [{1}] 印刷中にエラーが発生しました： {2}\r\n", DateTime.Now.ToString(), file, ex.Message);
                //File.AppendAllText(LOG_PATH + PRINT_ERROR_LOG, logMessage);
                throw;
            }
        }

        /// <summary>
        /// DISPLAY ERROR MESSAGE
        /// </summary>
        public static void DisplayError()
        {
            string msg_end = "\nプログラムを再起動してください。";
            //break & display error
            Status.Invoke(new MethodInvoker(delegate ()
            {
                Status.Text = "エラー";
                Status.BackColor = Color.Red;
            }));

            ErrMsg.Invoke(new MethodInvoker(delegate ()
            {
                string message = string.Format("[{0}]\n{1}\n{2}{3}", DateTime.Now.ToString(), ERROR_MSG, ERROR_DETAIL, msg_end);
                ErrMsg.Text = message;
                ErrMsg.Visible = true;
            }));

            MessageBox.Show(ERROR_MSG + msg_end, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    public class PrintList
    {
        public string Order_no { get; set; }
        public string Denpyo_flg { get; set; }
        public string Hikae_flg { get; set; }
        public string Receipt_flg { get; set; }
        public string Order_flg { get; set; }
        public string Label_flg { get; set; }

    }
    public class PdfFile
    {
        public string Printer_nm { get; set; }

        public byte[] Pdf { get; set; }

        public string Error { get; set; }
    }
    public class PrintHistory
    {
        public string User_nm { get; set; }
        public string Order_no { get; set; }
        public string Tokuisaki_nm { get; set; }
        public string Sale_dt { get; set; }
        public string Label_flg { get; set; }
        public string Denpyo_flg { get; set; }
        public string Hikae_flg { get; set; }
        public string Receipt_flg { get; set; }
        public string Order_flg { get; set; }
        public string Print_flg { get; set; }
    }
}