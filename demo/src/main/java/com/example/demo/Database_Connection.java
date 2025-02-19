package com.example.demo;

import java.sql.Connection;
import java.sql.Driver;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Database_Connection {
    private final static String URL = "jdbc:mysql://localhost:3306/users";
    private final static String UserName = "root";
    private final static String Password = "";

    public static Connection getConnection() throws SQLException{
        return DriverManager.getConnection(URL, UserName, Password);
    }
}
