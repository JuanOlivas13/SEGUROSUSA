using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEGUROSUSA
{
    public partial class navigator : Panel
    {
        public navigator()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void NavigateTo(UserControl View)
        {
            Controls.Clear();
            Controls.Add(View);
        }

        public void ClearNavigator()
        {
            Controls.Clear();
        }
    }
}
