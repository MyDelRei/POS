package com.example.demo;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;

import java.awt.print.PrinterGraphics;
import java.sql.*;


import java.sql.SQLException;
import java.util.PropertyPermission;

public class HelloController {

    public HelloController()
    {

    }
    @FXML
    private Label welcomeText;

    @FXML
    protected void onHelloButtonClick() {
        welcomeText.setText("Welcome to JavaFX Application!");
    }

    @FXML
    public Button btnAdd;

    @FXML
    public Button btnAdd11;

    @FXML
    public TextField inputName;

    @FXML
    public TextField inputEmail;

    @FXML
    public Label idLabel;

    @FXML
    public void action(){
        btnAdd.setOnAction(E ->
        {
            String name = inputName.getText();
            String email = inputEmail.getText();

            if(name.isEmpty() || email.isEmpty()){
                MessageBox.Show.Error("Please fill all in", "Missing information");
                return;
            }

            try {
                Person.insertPerson(name, email);
                loadData();
            }catch (NumberFormatException e){
                MessageBox.Show.Error("Error adding person: " + e.getMessage(), "Database Error");
            }
        });
    }
    @FXML
    private TableView <Person> tableView;

    @FXML
    private TableColumn <Person, Integer> idColumn;

    @FXML
    private TableColumn <Person, String> nameColumn;

    @FXML
    private TableColumn <Person, String> emailColumn;

    @FXML
    private ObservableList<Person> personList = FXCollections.observableArrayList();

    @FXML
    public Button btnDelete;

    @FXML
    public void initialize() {
        initializePersonColumns();
        loadData();

        // Add row selection listener
        tableView.setOnMouseClicked(event -> handleRowClick());

        // Set up dynamic search functionality
        searchField.textProperty().addListener((observable, oldValue, newValue) -> {
            performSearch(newValue);
        });
    }
    private void initializePersonColumns(){
        idColumn.setCellValueFactory(new PropertyValueFactory<>("id"));
        nameColumn.setCellValueFactory(new PropertyValueFactory<>("name"));
        emailColumn.setCellValueFactory(new PropertyValueFactory<>("email"));
    }
    @FXML
    private void loadData() {
       personList = Person.loadPerson();
       tableView.setItems(personList);
    }
    private void handleRowClick() {
        // Get the selected person
        Person selectedPerson = tableView.getSelectionModel().getSelectedItem();
        if (selectedPerson != null) {
            // Update the label and text fields
            idLabel.setText(String.valueOf(selectedPerson.getId())); // Show the selected ID
            inputName.setText(selectedPerson.getName()); // Populate inputName
            inputEmail.setText(selectedPerson.getEmail()); // Populate inputEmail
        }
    }

    @FXML
    public void updateAction() {
        btnAdd11.setOnAction(e -> {
            Person selectedPerson = tableView.getSelectionModel().getSelectedItem();
            if (selectedPerson == null) {
                MessageBox.Show.Error("Please select a person to update.", "Selection Error");
                return;
            }
            String id = idLabel.getText();

            int ID = Integer.parseInt(id);
            String name = inputName.getText();
            String email = inputEmail.getText();

            if (name.isEmpty() || email.isEmpty()) {
                MessageBox.Show.Error("Please fill all fields.", "Missing Information");
                return;
            }

            try {
                // Update the selected person in the database
                Person.updatePerson(ID, name, email);

                loadData(); // Refresh the table view with updated data
                clearFields(); // Clear input fields after update
            } catch (NumberFormatException ex) {
                MessageBox.Show.Error("Error updating person: " + ex.getMessage(), "Database Error");
            }
        });
    }

    @FXML
    public void deletePeron ()
    {
        Person selectPeron = tableView.getSelectionModel().getSelectedItem();

        if(selectPeron == null)
        {
            MessageBox.Show.Error("Please select person to delete", "Error");
            return;
        }

        try {
            String id = idLabel.getText();
            int ID = Integer.parseInt(id);
            Person.deletePerson(ID);

            loadData();
            clearFields();
        }
        catch (NumberFormatException e)
        {
            MessageBox.Show.Error(e.getMessage(), "Error");
        }
    }
    @FXML
    public Button btnAdd2;

    @FXML
    public void clearFields() {
        inputName.clear();
        inputEmail.clear();
        idLabel.setText("0");
    }
    @FXML
    private TextField searchField;



    private void performSearch(String searchTerm) {
        ObservableList<Person> filteredList;
        if (searchTerm.isEmpty()) {
            // If the search field is empty, load all data
            filteredList = Person.loadPerson();
        } else {
            // Otherwise, perform the search
            filteredList = Person.searchPerson(searchTerm);
        }
        tableView.setItems(filteredList); // Update the table view
    }

}