﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using assessment_platform_developer.Validation;
using Container = SimpleInjector.Container;

namespace assessment_platform_developer
{
    public partial class Customers : Page
    {
        private List<Customer> customers
        {
            // Changed customers storage to ViewState for better state management
            get { return ViewState["Customers"] as List<Customer> ?? new List<Customer>(); }
            set { ViewState["Customers"] = value; }
        }
        // Moved customer retrieval logic to ensure ViewState is used consistently
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerService>();
                customers = customerService.GetAllCustomers().ToList();
                PopulateCustomerDropDownLists();
                PopulateCustomerListBox();
            }
        }
        // Ensures 'Add new customer' is always an option in the dropdown
        private void PopulateCustomerListBox()
        {
            CustomersDDL.Items.Clear();
            CustomersDDL.Items.Add(new ListItem("Add new customer"));

            if (customers.Any())
            {
                CustomersDDL.Items.AddRange(customers
                    .Select(c => new ListItem(c.Name, c.ID.ToString()))
                    .ToArray());
            }
        }
        // Handles dropdown selection changes to populate form fields dynamically
        protected void CustomersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomersDDL.SelectedIndex > 0)
            {
                var customerId = int.Parse(CustomersDDL.SelectedValue);
                var customer = customers.FirstOrDefault(c => c.ID == customerId);

                if (customer != null)
                {
                    PopulateFormFields(customer);
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                    AddButton.Enabled = false;
                }
            }
            else
            {
                ClearForm();
                UpdateButton.Enabled = false;
                DeleteButton.Enabled = false;
                AddButton.Enabled = true;
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var newCustomer = CreateCustomerFromForm();
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerService>();
                customerService.AddCustomer(newCustomer);

                customers = customerService.GetAllCustomers().ToList();
                PopulateCustomerListBox();
                ClearForm();
            }
        }

        // Updates customer details and refreshes customer list
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && CustomersDDL.SelectedIndex > 0)
            {
                var customerId = int.Parse(CustomersDDL.SelectedValue);
                var existingCustomer = customers.FirstOrDefault(c => c.ID == customerId);

                if (existingCustomer != null)
                {
                    UpdateCustomerFromForm(existingCustomer);
                    var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                    var customerService = testContainer.GetInstance<ICustomerService>();
                    customerService.UpdateCustomer(existingCustomer);

                    customers = customerService.GetAllCustomers().ToList();
                    PopulateCustomerListBox();
                    ClearForm();
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                    AddButton.Enabled = true;
                }
            }
        }

        // Deletes selected customer and updates the dropdown list
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (CustomersDDL.SelectedIndex > 0)
            {
                var customerId = int.Parse(CustomersDDL.SelectedValue);
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerService>();
                customerService.DeleteCustomer(customerId);

                customers = customerService.GetAllCustomers().ToList();
                PopulateCustomerListBox();
                ClearForm();
                UpdateButton.Enabled = false;
                DeleteButton.Enabled = false;
                AddButton.Enabled = true;
            }
        }

        private Customer CreateCustomerFromForm()
        {
            return new Customer
            {
                Name = CustomerName.Text,
                Address = CustomerAddress.Text,
                Email = CustomerEmail.Text,
                Phone = CustomerPhone.Text,
                City = CustomerCity.Text,
                State = StateDropDownList.SelectedValue,
                Zip = CustomerZip.Text,
                Country = CountryDropDownList.SelectedValue,
                Notes = CustomerNotes.Text,
                ContactName = ContactName.Text,
                ContactPhone = ContactPhone.Text,
                ContactEmail = ContactEmail.Text
            };
        }

        private void UpdateCustomerFromForm(Customer customer)
        {
            customer.Name = CustomerName.Text;
            customer.Address = CustomerAddress.Text;
            customer.Email = CustomerEmail.Text;
            customer.Phone = CustomerPhone.Text;
            customer.City = CustomerCity.Text;
            customer.State = StateDropDownList.SelectedValue;
            customer.Zip = CustomerZip.Text;
            customer.Country = CountryDropDownList.SelectedValue;
            customer.Notes = CustomerNotes.Text;
            customer.ContactName = ContactName.Text;
            customer.ContactPhone = ContactPhone.Text;
            customer.ContactEmail = ContactEmail.Text;
        }

        private void PopulateFormFields(Customer customer)
        {
            CustomerName.Text = customer.Name;
            CustomerAddress.Text = customer.Address;
            CustomerEmail.Text = customer.Email;
            CustomerPhone.Text = customer.Phone;
            CustomerCity.Text = customer.City;
            StateDropDownList.SelectedValue = customer.State;
            CustomerZip.Text = customer.Zip;
            CountryDropDownList.SelectedValue = customer.Country;
            CustomerNotes.Text = customer.Notes;
            ContactName.Text = customer.ContactName;
            ContactPhone.Text = customer.ContactPhone;
            ContactEmail.Text = customer.ContactEmail;
        }

        // Resets all form fields, including contact details
        private void ClearForm()
        {
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactPhone.Text = string.Empty;
            ContactEmail.Text = string.Empty;
        }

        private void PopulateCustomerDropDownLists()
        {

            var countryList = Enum.GetValues(typeof(Countries))
                .Cast<Countries>()
                .Select(c => new ListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                })
                .ToArray();

            CountryDropDownList.Items.AddRange(countryList);
            CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();


            var provinceList = Enum.GetValues(typeof(CanadianProvinces))
                .Cast<CanadianProvinces>()
                .Select(p => new ListItem
                {
                    Text = p.ToString(),
                    Value = ((int)p).ToString()
                })
                .ToArray();

            StateDropDownList.Items.Add(new ListItem(""));
            StateDropDownList.Items.AddRange(provinceList);
        }
        // Validates postal/zip code based on selected country using the validator function
        protected void cvCustomerZip_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string zip = args.Value;
            string country = CountryDropDownList.SelectedItem.Text;

            var validator = new ZipCodeValidator();
            args.IsValid = validator.IsValid(zip, country);
        }
    }
}