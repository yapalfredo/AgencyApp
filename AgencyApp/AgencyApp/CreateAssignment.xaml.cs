using AgencyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgencyApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateAssignment : ContentPage
	{
        Assignments assignment = new Assignments();
		public CreateAssignment ()
		{
			InitializeComponent ();            
            pickerClientName.ItemsSource = ViewQueries.ClientUserIDView();
            pickerClientName.ItemDisplayBinding = new Binding("Name");
            pickerContractorName.ItemsSource = ViewQueries.ContractorUserIDView();
            pickerContractorName.ItemDisplayBinding = new Binding("Name");
            stackLayoutContainer.BindingContext = assignment;
        }

        private async void ButtonCreate_Clicked(object sender, EventArgs e)
        {
            if ((pickerClientName.SelectedIndex < 0) &&
                (pickerContractorName.SelectedIndex < 0) &&
                (pickerSelectShift.SelectedIndex < 0) &&
                (!switchMon.IsToggled && !switchTue.IsToggled &&
                !switchWed.IsToggled && !switchThu.IsToggled &&
                !switchFri.IsToggled && !switchSat.IsToggled &&
                !switchSun.IsToggled))
            {
               await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                assignment.AgencyId = App.userID;
                var client = pickerClientName.SelectedItem as ViewQueries;
                assignment.ClientId = client.Id;
                assignment.StartDate = DateTime.Now.ToShortDateString();
                assignment.Shift = pickerSelectShift.SelectedItem as string;

                assignment.ClockIn = timePickerStartTime.Time.ToString();
                assignment.ClockOut = timePickerEndTime.Time.ToString();
                if (pickerSelectShift.SelectedItem as string == "Day Shift")
                {
                    var contractor = pickerContractorName.SelectedItem as ViewQueries;
                    assignment.DayShiftContractorID = contractor.Id;
                }else if (pickerSelectShift.SelectedItem as string == "Night Shift")
                {
                    var contractor = pickerContractorName.SelectedItem as ViewQueries;
                    assignment.NightShiftContractorID = contractor.Id;
                }
                assignment.Active = true;
                try
                {
                    Create(assignment);
                }
                catch (NullReferenceException) { }
                catch (Exception) { }

                await DisplayAlert("Successful", "Job assignment created", "Ok");
                ViewQueries.ClearFields(this.Content);
            }

             async void Create(Assignments assignment)
            {
                await App.MobileService.GetTable<Assignments>().InsertAsync(assignment);               
                await Assignments.Refresh();                
            }
        }
    }
}