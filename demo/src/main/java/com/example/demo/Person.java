package com.example.demo;
import java.sql.*;

import com.mysql.cj.jdbc.CallableStatement;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.chart.PieChart;

import java.sql.PreparedStatement;
import java.util.PropertyPermission;

public class Person {
    private String name;
    private int id;
    private String email;

    public Person (int id, String name, String email) {
        this.email = email;
        this.id = id;
        this.name = name;
    }

    public int getId() { return id; }
    public String getName() { return name; }
    public String getEmail() { return email; }

    public static void insertPerson(String name, String email){
    String query = "INSERT INTO users (name, email) VALUES (?, ?)";

    try {
        Connection conn = Database_Connection.getConnection();

        PreparedStatement cmd = conn.prepareStatement(query);
        cmd.setString(1,name);
        cmd.setString(2, email);

        int rowEffect = cmd.executeUpdate();
        if(rowEffect > 0){
            MessageBox.Show.Information("Information has been successfully inserted!", "success");
        }
        else {
            MessageBox.Show.Error("Opss! something went wrong", "Error");
        }
    } catch (SQLException e) {
        throw new RuntimeException(e);
    }
    }

    public static ObservableList<Person> searchPerson (String searchTerm){
        ObservableList<Person> filtteredList = FXCollections.observableArrayList();
        String searchQuery = "SELECT id, name, email FROM users WHERE name LIKE ? OR email LIKE ?";

        try{
            Connection conn = Database_Connection.getConnection();
            PreparedStatement statement = conn.prepareStatement(searchQuery);

        statement.setString(1, "%" + searchTerm  + "%");
        statement.setString(2, "%" + searchTerm + "%");

        ResultSet resultSet = statement.executeQuery();

        while (resultSet.next()){
            filtteredList.add(new Person(
                    resultSet.getInt("id"),
                    resultSet.getString("name"),
                    resultSet.getString("email")
            ));
        }

        }catch (NumberFormatException e)
        {
            MessageBox.Show.Error("Error while searching " + e.getMessage(), "Error");
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return filtteredList;
    }

    public static ObservableList<Person> loadPerson()  {
        ObservableList<Person> personList = FXCollections.observableArrayList();
        String query = "SELECT id, name, email FROM users";

        try (Connection conn = Database_Connection.getConnection();
             Statement statement = conn.createStatement();
             ResultSet resultSet = statement.executeQuery(query)) {

            while (resultSet.next()) {
                personList.add(new Person(
                        resultSet.getInt("id"),
                        resultSet.getString("name"),
                        resultSet.getString("email")
                ));
            }
        } catch (SQLException e) {
            MessageBox.Show.Error("Oops, something went wrong: " + e.getMessage(), "Error");
        }

        return personList; // Return the populated list
    }
    public static void updatePerson(int id, String name, String email)
    {
        String query = "UPDATE users SET name = ?, email = ? WHERE id = ?";

        try{
            Connection conn = Database_Connection.getConnection();
            PreparedStatement statement = conn.prepareStatement(query);
            {
                statement.setString(1, name);
                statement.setString(2, email);
                statement.setInt(3, id);

                int rowsAffected = statement.executeUpdate();
                if (rowsAffected > 0) {
                    MessageBox.Show.Information("Record updated successfully!", "Success");
                } else {
                    MessageBox.Show.Error("No record found to update.", "Error");
                }
            }
        }
        catch (SQLException e)
        {
            MessageBox.Show.Error(e.getMessage(), "Error");
        }
    }

    public static void deletePerson(int ID){
        String query = "DELETE FROM users WHERE id = ?";

        try { Connection conn = Database_Connection.getConnection();
            PreparedStatement statement = conn.prepareStatement(query);

            statement.setInt(1, ID);
            int rowEffect = statement.executeUpdate();

            if(rowEffect > 0)
            {
                MessageBox.Show.Information("Delete successfully!", "Success");
            }
            else
            {
                MessageBox.Show.Error("Opps! something went wrong", "Error");
            }

        }catch (SQLException e)
        {
            MessageBox.Show.Error(e.getMessage(), "Error");
        }
    }
}
