using Newtonsoft.Json;
using QuanLyNhaHang.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fVnPayQrCode : Form
    {
        public fVnPayQrCode(float tongTien)
        {
            InitializeComponent();
            if (tongTien != null)
            {
                nmMoney.Value = (decimal)tongTien;
            }
            else
            {
                MessageBox.Show("Bạn cần có hóa đơn thanh toán");
                this.Close();
            }
            txbTenTaiKhoan.Text = "TRINH VINH CUONG";
            txbSoTaiKhoan.Text = "0846576076";
            using (WebClient client = new WebClient())
            {
                var htmlData = client.DownloadData("https://api.vietqr.io/v2/banks");
                var bankRawJson = Encoding.UTF8.GetString(htmlData);
                var ListBankData = JsonConvert.DeserializeObject<Banks>(bankRawJson);

                cbNganHang.DataSource = ListBankData.data;    // list banks
                cbNganHang.DisplayMember = "customName";
                cbNganHang.ValueMember = "bin";
                cbNganHang.SelectedItem = ListBankData.data.FirstOrDefault(); // Chọn ngân hàng đầu tiên trong danh sách
                cbTemplate.SelectedIndex = 0;
            }
        }
        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            var apiRequest = new APIRequest();
            // Fix: Replace 'EditValue' with 'SelectedValue' to correctly retrieve the selected value from the ComboBox.
            apiRequest.acqId = Convert.ToInt32(cbNganHang.SelectedValue.ToString());
            apiRequest.accountNo = long.Parse(txbSoTaiKhoan.Text);
            apiRequest.accountName = txbTenTaiKhoan.Text;
            apiRequest.amount = Convert.ToInt32(nmMoney.Text);
            apiRequest.format = "text";
            apiRequest.template = cbTemplate.Text;
            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            // use restsharp for request api.
            var client = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest();

            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content;
            var dataResult = JsonConvert.DeserializeObject<APIReponse>(content);

            var image = Base64ToImage(dataResult.data.qrDataURL.Replace("data:image/png;base64,", ""));
            pbQR.Image = image;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
