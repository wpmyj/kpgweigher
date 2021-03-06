﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ioex_cs
{
    /// <summary>
    /// Interaction logic for BottomWnd.xaml
    /// </summary>
    public partial class BottomWnd : Window
    {
        private UIPacker curr_packer
        {
            get
            {
                App p = Application.Current as App;
                return p.packers[0];
            }
        }
        public BottomWnd()
        {
            InitializeComponent();
            
        }
        public void UpdateDisplay()
        {
            
            Intf i = curr_packer.getInterface();
            curr_packer.agent.SetVibIntf(curr_packer.vib_addr,i);
            intf_ckb_mem.IsChecked = i.b_Hasmem;
            intf_handshake.IsChecked = i.b_Handshake;
            intf_ckb_delay.Content = i.delay_length.ToString();
            intf_lb_feed_times.Content = (i.feed_times+1).ToString();
            intf_input_trigger.SelectedIndex = i.fmt_input;
            intf_output_trigger.SelectedIndex = i.fmt_output;
            intf_pulse_width.Content = curr_packer.agent.GetNodeReg(curr_packer.bot_addr, "cs_filter").ToString();
        }
        private void ApplySetting()
        {
            App p = Application.Current as App;
            Intf i = new Intf(0);
            UInt16 delay = UInt16.Parse(intf_ckb_delay.Content.ToString());

            i.b_Hasdelay = (delay > 0);
            i.b_Hasmem = intf_ckb_mem.IsChecked.Value;
            i.b_Handshake = intf_handshake.IsChecked.Value;
            i.delay_length = delay;
            i.feed_times =Convert.ToUInt16(UInt16.Parse(intf_lb_feed_times.Content.ToString()) - 1);
            i.fmt_input = Convert.ToUInt16(intf_input_trigger.SelectedIndex);
            i.fmt_output = Convert.ToUInt16(intf_output_trigger.SelectedIndex);
            curr_packer.setInterface(i);
            curr_packer.agent.SetVibIntf(curr_packer.vib_addr, i);
            curr_packer.agent.SetNodeReg(curr_packer.bot_addr, "cs_filter", Convert.ToUInt16(intf_pulse_width.Content));
            UpdateDisplay();
        }
        private void btn_return_Click(object sender, RoutedEventArgs e)
        {
            App p = Application.Current as App;

            ApplySetting();
            
            curr_packer.agent.SetNodeReg(curr_packer.bot_addr, "cs_filter", Convert.ToUInt16(intf_pulse_width.Content));
            curr_packer.SaveCurrentConfig(2);
            p.SwitchTo("configmenu");
            
        }
        private void btn_ret_run_Click(object sender, RoutedEventArgs e)
        {
            App p = Application.Current as App;
            Intf i = new Intf(0);
            UInt16 delay = UInt16.Parse(intf_ckb_delay.Content.ToString());

            ApplySetting();
            
            curr_packer.SaveCurrentConfig(2);
            p.SwitchTo("runmode");

        }
        
        private void node_reg(string regname)
        {
            (Application.Current as App).kbdwnd.Init(StringResource.str("enter_" + regname), regname, false, KbdData);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            
            if (sender is Label)
            {
                Label t = sender as Label;
                if (t.Name == "intf_lb_feed_times" || t.Name == "intf_ckb_delay" || t.Name == "intf_pulse_width")
                {
                    node_reg(t.Name);
                }
            }
        }
        public void KbdData(string param, string data)
        {
            if (param == "intf_ckb_delay")
            {
                intf_ckb_delay.Content = data;
            }
            if (param == "intf_lb_feed_times")
            {
                intf_lb_feed_times.Content = data;
            }
            if (param == "intf_pulse_width")
            {
                intf_pulse_width.Content = data;
            }
            ApplySetting();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            App p = Application.Current as App;
            this.Hide();
            if((sender as Button).Name == "btn_ret")
                p.SwitchTo("configmenu");
            else
                p.SwitchTo("runmode");
            
        }

        private void btn_run_Click(object sender, RoutedEventArgs e)
        {
            App p = Application.Current as App;
            curr_packer.BottomAction("trigger");
        }

        private void intf_input_trigger_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ApplySetting();
        }

        private void intf_handshake_Click(object sender, RoutedEventArgs e)
        {
            ApplySetting();
        }    
    }
}
