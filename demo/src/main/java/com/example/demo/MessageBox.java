package com.example.demo;

import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;

public class MessageBox {
    public static class Show {
        public static void Error(String message, String title) {
            Alert alert = new Alert(Alert.AlertType.ERROR, message, ButtonType.OK);
            alert.setTitle(title);
            alert.setHeaderText(null);
            alert.showAndWait();
        }

        public static void Information(String message, String title) {
            Alert alert = new Alert(Alert.AlertType.INFORMATION, message, ButtonType.OK);
            alert.setTitle(title);
            alert.setHeaderText(null);
            alert.showAndWait();
        }
    }
}
