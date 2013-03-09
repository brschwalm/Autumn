var contactModel = function (name, age, email) {
    var self = this;
    self.id = ko.observable(0);
    self.name = ko.observable(name);
    self.age = ko.observable(age);
    self.email = ko.observable(email);
    self.addressId = ko.observable(-1);
    self.address = ko.observable(addressModel);

    self.isNew = ko.observable(false);
    self.isEditing = ko.observable(false);
    self.isWorking = ko.observable(false);

    self.original = null;

    self.beginEdit = function () {
        self.original = new contactModel(self.name(), self.age(), self.email());
        self.isEditing(true);
    }
    self.commitEdit = function () {
        self.original = null;
        self.isEditing(false);
        self.isWorking(false);
    }
    self.rollbackEdit = function () {
        self.name(self.original.name());
        self.age(self.original.age());
        self.email(self.original.email());
        self.original = null;
        self.isEditing(false);
        self.isWorking(false);
    }
}

var addressModel = function (street1, street2, city, state, zip) {
    var self = this;
    self.id = ko.observable(0);
    self.street1 = ko.observable(street1);
    self.street2 = ko.observable(street2);
    self.city = ko.observable(city);
    self.state = ko.observable(state);
    self.zip = ko.observable(zip);
}

var viewModel = function () {
    var self = this;

    self.Contacts = ko.observableArray([]);
    self.isWorking = ko.observable(false);

    //Load the contacts for this VM.
    self.loadContacts = function (callback) {
        self.isWorking(true);
        $.getJSON(API_URL,
                  function (data) {
                      convertContacts(self, data);
                      if (callback != null) callback();
                      viewModel.isWorking(false);
                  });
    }

    self.addContact = function (vm) {
        contact = new contactModel();
        contact.isNew(true);
        contact.beginEdit();
        vm.Contacts.push(contact);
    }

    self.editContact = function (contact) {
        contact.beginEdit();
    }

    self.saveChanges = function (contact) {
        if (contact.isNew()) viewModel.saveNewContact(contact);
        else if (contact.isEditing()) viewModel.saveExistingContact(contact);
    }

    self.cancelChanges = function (contact) {
        if (contact.isNew()) {
            viewModel.Contacts.pop();
        }
        else {
            contact.rollbackEdit();
            contact.isEditing(false);
        }
    }

    self.saveNewContact = function (contact) {

        var jsContact = ko.toJSON(contact);
        contact.isWorking(true);

        $.ajax(API_URL,
               {
                   type: 'post',
                   data: jsContact,
                   contentType: 'application/json',
                   success: function (result) {
                       contact.id(result.Id);
                       contact.isNew(false);
                       contact.commitEdit();
                   }
               });
    }

    self.saveExistingContact = function (contact) {

        var jsContact = ko.toJSON(contact);
        contact.isWorking(true);

        $.ajax(API_URL + '?id=' + contact.id(),
            {
                type: 'put',
                data: jsContact,
                contentType: 'application/json',
                success: function (result) {
                    contact.commitEdit();
                }
            });
    }

    self.contactToDelete = ko.observable(null);

    self.deleteContact = function (contact) {
        self.contactToDelete(contact);
        confirmAction('Delete Contact', 'Are you sure you would like to delete this contact?', self.doDeleteContact, function () { self.contactToDelete(null); });
    }
    self.doDeleteContact = function(){
        $.ajax(API_URL + '?id=' + self.contactToDelete().id(),
            {
                type: 'delete',
                success: function(result){
                    self.Contacts.destroy(self.contactToDelete());
                    self.contactToDelete(null);
                },
                complete: function(result, status){
                    self.isWorking(false);
                }
            });
    }
}

function convertContacts(vm, data) {
    for (i = 0; i < data.length; i++) {
        var c = mapContact(data[i]);
        vm.Contacts.push(c);
    }
}

function mapContact(source) {

    var target = new contactModel(source.Name, source.Age, source.Email);
    target.id(source.Id);
    if (source.Address != null) {
        target.address(mapAddress(source.Address));
        target.addressId(target.address.id);
    }

    return target;
}

function mapAddress(source) {
    var target = new addressModel(source.Street1, source.Street2, source.City, source.State, source.Zip);
    return target;
}