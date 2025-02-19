package com.example.demo;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

import java.io.IOException;

public class HelloApplication extends Application {
    @Override
    public void start(Stage primaryStage) throws Exception {
        // Load the login screen first
        FXMLLoader loginLoader = new FXMLLoader(getClass().getResource("hello-view.fxml"));
        AnchorPane loginRoot = loginLoader.load();
        Scene loginScene = new Scene(loginRoot);

        // Set up the login scene
        primaryStage.setTitle("Login");
        primaryStage.setScene(loginScene);
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch();
    }
}