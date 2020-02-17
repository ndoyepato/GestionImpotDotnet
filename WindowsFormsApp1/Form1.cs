using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        List<Habitation> habitations = new List<Habitation>();
        Habitation currentHabitation = new Habitation();

        // API UTILS
        static HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:8080/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void generateHabitations()
        {

            for(int i=0; i<20; i++)
            {
                Habitation habitation = new Habitation();
                habitation.Proprietaire = "Issa Ndoye Mbengue"+i;
                habitation.Adresse = "Pikine"+i;
                habitation.Surface = "21"+i;
                habitation.NbrePiece = i+"";
                habitations.Add(habitation);
            }
        }

        private async Task button3_ClickAsync(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Veuiller renseigner le proprietaire svp.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Veuiller renseigner l'adresse svp.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("Veuiller renseigner la surface svp.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox4.Text == "")
            {
                MessageBox.Show("Veuiller renseigner le nombre de pièce svp.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Habitation habitation = new Habitation();
            habitation.Proprietaire = textBox1.Text;
            habitation.Adresse = textBox2.Text;
            habitation.Surface = textBox3.Text;
            habitation.NbrePiece = textBox4.Text;

            DialogResult result = MessageBox.Show("Voulez-vous vraiment Enregistrer", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(result == DialogResult.OK)
            {
                MessageBox.Show("Enregistrement en cours..");
                var response = await client.PostAsJsonAsync("addHabitation", habitation);
            }
        }

        private async Task button2_Click_1Async(object sender, EventArgs e)
        {
            if (currentHabitation != null)
            {
                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette habitation?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(result == DialogResult.OK)
                {
                    MessageBox.Show("Suppression en cours...");
                    var response = await client.PostAsJsonAsync("deleteHabitation", currentHabitation);
                }
            }
            else
            {
                MessageBox.Show("Aucune habitation selectionnée", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task button1_ClickAsync(object sender, EventArgs e)   
        {
            if (currentHabitation != null)
            {
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment modifier cette habitation?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(dialogResult == DialogResult.OK)
                {
                    MessageBox.Show("Modification en cours...");
                    var response = await client.PutAsJsonAsync("updateHabitation", currentHabitation);
                }
            }
            else
            {
                MessageBox.Show("Aucune habitation selectionnée", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
