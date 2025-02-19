package com.example.demo;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.PasswordField;
import javafx.fxml.FXML;
import javafx.fxml.FXML;
import javafx.scene.control.TextField;
import javafx.scene.control.Alert;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

import java.awt.*;

public class LoginController {
    @FXML
    private TextField usernameField;

    @FXML
    private PasswordField passwordField;

    @FXML
    private void handleLogin() {
        String username = usernameField.getText();
        String password = passwordField.getText();


        if (username.isEmpty() || password.isEmpty()) {
            MessageBox.Show.Error("Please fill in both username and password.", "Login Error");
        } else if (username.equals("admin") && password.equals("12345")) { // Example validation
            MessageBox.Show.Information("Login successful!", "Success");
            loadMainView();
        } else {
            MessageBox.Show.Error("Invalid credentials.", "Login Error");
        }
    }

    private void loadMainView() {
        try {
            // Load the main view (hello-view.fxml)
            FXMLLoader loader = new FXMLLoader(getClass().getResource("hello-view.fxml"));
            AnchorPane root = loader.load();

            // Get the Stage (the current window) and set the new scene
            Stage stage = (Stage) usernameField.getScene().getWindow();
            Scene scene = new Scene(root);
            stage.setScene(scene);
            stage.setTitle("Hello View");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
