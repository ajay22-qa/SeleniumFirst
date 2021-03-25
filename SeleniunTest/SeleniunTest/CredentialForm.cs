using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using FacebookMarketplace.DbHelper;
using FacebookMarketplace.Models;

namespace FacebookMarketplace
{
	public partial class CredentialForm : Form
	{
		public CredentialForm()
		{
			InitializeComponent();
		}
		public List<FacebookUserLoginInfo> loginAllUser
		{
			get;
			set;
		}
		public static ComboBox CmbUser { get; set; }
		//OnlineFetchData fetchData = new OnlineFetchData();
		private void CredentialForm_Load(object sender, EventArgs e)
		{
			GetDisplay();
		}

		private void GetDisplay()
		{
			if (listView1 != null)
			{
				listView1.Items.Clear();

				//loginAllUser = SqlHelper.GetLoginUsers();
				if (loginAllUser.Count > 0)
				{

					display();

				}
			}
			else
			{

				MessageBox.Show("List is Empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		public void display()
		{
			foreach (var item in loginAllUser)
			{
				var items = new ListViewItem();
				items.SubItems[0].Text = item.UserId;
				items.SubItems.Add(item.LoginUserName);
				items.SubItems.Add(item.Password);
				listView1.Items.Add(items);
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			try
			{
				if (listView1.SelectedItems.Count > 0)
				{
					string selectUerEmailId = txtUserName.Text.TrimStart();
					string selectedUserPassword = txtPassword.Text;

					if (
						!(string.IsNullOrEmpty(selectUerEmailId) || string.IsNullOrEmpty(selectedUserPassword) ||
						  (!(listView1.SelectedItems.Count > 0))))
					{
						if (
							MessageBox.Show("Do you want to Update this record?", "Entry Saving",
								MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							int selectUserId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text.ToString());
							//int result = SqlHelper.UpdateLoginUser(selectUserId, selectUerEmailId, selectedUserPassword);
							//if (result > 0)
							{
								MessageBox.Show("User Update successfully.!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

							}
							GetDisplay();
							DefaultSetting();
						}
						else
						{
							txtUserName.Text = "";
							txtPassword.Text = "";
							return;
						}
					}
					else
					{
						MessageBox.Show("Please select listview value for Update ", Application.ProductName,
							MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtUserName.Text = "";
						txtPassword.Text = "";
						return;
					}
				}
				else
				{

					MessageBox.Show("Please select listview value for update User", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtUserName.Text = "";
					txtPassword.Text = "";
					return;
				}
			}
			catch (Exception)
			{

			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				string TxtUserId = txtUserName.Text.TrimStart();
				string TxtPassword = txtPassword.Text;
				if (!(string.IsNullOrEmpty(TxtUserId)))
				{
					if (!string.IsNullOrEmpty(TxtPassword))
					{
						if (MessageBox.Show("Do you want to save this record?", "Entry Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							SaveRecord(TxtUserId, TxtPassword);

						}
						if (CmbUser != null)
						{
						//	fetchData.dropDrownValue(CmbUser);
						}

					}
					else
					{
						MessageBox.Show("Please enter Password", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
				}
				else
				{
					MessageBox.Show("Please enter UserName", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			catch (Exception)
			{
			}
		}
		//FbMarketplaceHelper sql = new FbMarketplaceHelper();
		private void SaveRecord(string TxtUserId, string TxtPassword)
		{
			string sqlQuery = "select count(*) from TblLogin where UserId='" + TxtUserId + "'  ";
			//int count = Convert.ToInt32(sql.ExecuteScalar(sqlQuery));
			if (count == 0)
			{
				string query = "INSERT INTO TblLogin(UserId,Password) values('" + TxtUserId + "','" + TxtPassword + "') ";
				//int yy = sql.ExecuteNonQuery(query);
				GetDisplay();
				DefaultSetting();
			}
			else
			{
				string sqlsaveQuery = "select count(*) from TblLogin where UserId='" + TxtUserId + "' and Password='" + TxtPassword + "' ";
				//int countsave = Convert.ToInt32(sql.ExecuteScalar(sqlsaveQuery));
				//if (countsave > 0)
				{
					MessageBox.Show("Data is already exist!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					txtUserName.Text = "";
					txtPassword.Text = "";
					return;
				}
				string queryLoginUpdate = string.Format("update TblLogin set Password='{0}' where UserId='{1}'", TxtPassword, TxtUserId);
				//int yy = sql.ExecuteNonQuery(queryLoginUpdate);
				GetDisplay();
				DefaultSetting();
				MessageBox.Show("user data saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				txtUserName.Text = "";
				txtPassword.Text = "";
			}
		}

		private void DefaultSetting()
		{
			txtPassword.Text = string.Empty;
			txtUserName.Text = string.Empty;
			IEnumerable<ListViewItem> listView = listView1.Items.Cast<ListViewItem>();
			listView.ToList().ForEach(m => m.Selected = false);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Do you want to Delete this record?", "Entry Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int selectUserId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text.ToString());
					string selectUerEmailId = listView1.SelectedItems[0].SubItems[1].Text.Trim();
					//int result = SqlHelper.DeleteLoginUser(selectUserId, selectUerEmailId);
					//if (result == 0)
					{
						MessageBox.Show("User deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					listView1.Items.Clear();
					//loginAllUser = SqlHelper.GetLoginUsers();
					if (loginAllUser.Count > 0)
					{
						display();
					}
					else
					{
						txtUserName.Text = "";
						txtPassword.Text = "";
						return;
					}
				}
				else
				{
					txtUserName.Text = "";
					txtPassword.Text = "";
					return;
				}
				txtUserName.Text = "";
				txtPassword.Text = "";
			}
			else
			{
				MessageBox.Show("Please select listview value for delete User", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				txtUserName.Text = "";
				txtPassword.Text = "";
				return;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				string userName = listView1.SelectedItems[0].SubItems[1].Text;
				string Password = listView1.SelectedItems[0].SubItems[2].Text;
				txtUserName.Text = userName;
				txtPassword.Text = Password;
				// oldUserid = userName;
			}
		}

		public void add(ComboBox cmbFbUser)
		{
			CmbUser = cmbFbUser;
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
