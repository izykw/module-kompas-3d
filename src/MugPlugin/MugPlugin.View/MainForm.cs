﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MugPlugin.Model;

namespace MugPlugin.View
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Mug parameters.
        /// </summary>
        private readonly MugParameters Parameters;

        /// <summary>
        /// Stores a text field and its error.
        /// </summary>
        private readonly Dictionary<TextBox, string> TextBoxAndError;

        /// <summary>
        /// Stores a text field and its corresponding parameter type.
        /// </summary>
        private readonly Dictionary<TextBox, MugParametersType> TextBoxToParameterType;

        public MainForm()
        {
            InitializeComponent();
            Parameters = new MugParameters();
            TextBoxToParameterType = new Dictionary<TextBox, MugParametersType>
            {
                { diameter, MugParametersType.Diameter },
                { height, MugParametersType.Height },
                { thickness, MugParametersType.Thickness },
                { handleDiameter, MugParametersType.HandleLength },
                { handleLength, MugParametersType.HandleDiameter }
            };
            TextBoxAndError = new Dictionary<TextBox, string>
            {
                { diameter, "" },
                { height, "" },
                { thickness, "" },
                { handleDiameter, "" },
                { handleLength, "" }
            };
        }

        /// <summary>
        /// Sets default values ​​on form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetDefaultValues(87, 95, 7, 33.25, 66.5);
        }

        /// <summary>
        /// Sets parameter value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetParameter(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            var isType = TextBoxToParameterType.TryGetValue(textBox, out var type);
            double.TryParse(textBox.Text, out var value);

            if (!isType) return;

            try
            {
                Parameters.SetParameterValue(type, value);
                TextBoxAndError[textBox] = "";
                errorProvider.Clear();
            }
            catch (Exception error)
            {
                TextBoxAndError[textBox] = error.Message;
                errorProvider.SetError(textBox, error.Message);
            }
        }

        /// <summary>
        /// Clears a text field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTextBox(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        /// <summary>
        /// Sets the minimum parameters of the mug.
        /// </summary>
        private void SetMinParameters(object sender, MouseEventArgs e)
        {
            SetDefaultValues(70, 85, 5, 29.75, 59.5);
        }

        /// <summary>
        /// Sets the average parameters of the mug.
        /// </summary>
        private void SetAvgParameters(object sender, EventArgs e)
        {
            SetDefaultValues(87, 95, 7, 33.25, 66.5);
        }

        /// <summary>
        ///  Sets the maximum parameters for the mug.
        /// </summary>
        private void SetMaxParameters(object sender, EventArgs e)
        {
            SetDefaultValues(105, 130, 10, 45.5, 91);
        }

        /// <summary>
        /// Sets default values.
        /// </summary>
        /// <param name="diameterValue">Mug diameter.</param>
        /// <param name="heightValue">Mug height.</param>
        /// <param name="thicknessValue">Mug wall thickness.</param>
        /// <param name="handleLengthValue">Mug handle length.</param>
        /// <param name="handleDiameterValue">Mug handle diameter.</param>
        private void SetDefaultValues(double diameterValue, double heightValue,
            double thicknessValue, double handleLengthValue, double handleDiameterValue)
        {
            Parameters.SetParameterValue(MugParametersType.Diameter, diameterValue);
            Parameters.SetParameterValue(MugParametersType.Height, heightValue);
            Parameters.SetParameterValue(MugParametersType.Thickness, thicknessValue);
            Parameters.SetParameterValue(MugParametersType.HandleDiameter, handleDiameterValue);
            Parameters.SetParameterValue(MugParametersType.HandleLength, handleLengthValue);

            diameter.Text = diameterValue.ToString();
            height.Text = heightValue.ToString();
            thickness.Text = thicknessValue.ToString();
            handleDiameter.Text = handleLengthValue.ToString();
            handleLength.Text = handleDiameterValue.ToString();
        }

        /// <summary>
        /// Checks if all text fields are filled correctly.
        /// </summary>
        /// <returns></returns>
        private bool CheckTextBoxes()
        {
            var isError = true;
            foreach (var item in 
                     TextBoxAndError.Where(item => item.Value != ""))
            {
                isError = false;
                errorProvider.SetError(item.Key, item.Value);
            }

            return isError;
        }

        /// <summary>
        /// Build mug in Kompas 3D.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void build_click(object sender, EventArgs e)
        {
            if (CheckTextBoxes())
            {
                // Call build method
            }
            else
            {
                MessageBox.Show(@"Fill all required parameters correctly");
            }
        }
    }
}