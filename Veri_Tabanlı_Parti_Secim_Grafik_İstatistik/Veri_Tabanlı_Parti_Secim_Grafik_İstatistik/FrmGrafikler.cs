using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Veri_Tabanlı_Parti_Secim_Grafik_İstatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt=new SqlCommand("Select * From TBLILCE where ILCEAD=@P1",baglanti);
            kmt.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr3=kmt.ExecuteReader();
            while (dr3.Read())
            {
                progressBar1.Value = int.Parse(dr3[2].ToString());
                progressBar2.Value = int.Parse(dr3[3].ToString());
                progressBar3.Value = int.Parse(dr3[4].ToString());
                progressBar4.Value = int.Parse(dr3[5].ToString());
                progressBar5.Value = int.Parse(dr3[6].ToString());
                LblA.Text = dr3[2].ToString();
                LblB.Text = dr3[3].ToString();
                LblC.Text = dr3[4].ToString();
                LblD.Text = dr3[5].ToString();
                LblE.Text = dr3[6].ToString();
            }
            baglanti.Close();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = DBSECIMPROJE; Integrated Security = True");
        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) from TBLILCE", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);
            }
            baglanti.Close();
        }
    }
}
