﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmOsteomuscular : Form
    {
        private string _serviceId;
        private string _ComponentId;

        public frmOsteomuscular(string serviceId, string ComponentId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _ComponentId = ComponentId;
        }

        private void frmOsteomuscular_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crEvaluacionOsteomuscular();

            var aptitudeCertificate = new ServiceBL().ReportOsteoMuscular(_serviceId, _ComponentId);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtOstomuscular";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
