module com.example.demo {
    requires javafx.controls;
    requires javafx.fxml;

    requires com.dlsc.formsfx;
    requires java.sql;
    requires java.desktop;
    requires mysql.connector.j;

    opens com.example.demo to javafx.fxml;
    exports com.example.demo;
}