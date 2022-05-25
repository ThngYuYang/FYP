package FYPJavaFX;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.regex.Pattern;

import FYPJavaFX.adsdas.Item;
import javafx.application.Application;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.control.cell.TextFieldTableCell;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import javafx.util.Callback;
import javafx.collections.FXCollections;
import javafx.application.Application;
import javafx.beans.property.*;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.event.*;
import javafx.geometry.*;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.*;
import javafx.stage.*;
import javafx.util.Callback;
import javafx.application.Application;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;


/**
 * I declare that this code was written by me.
 * I will not copy or allow others to copy my code.
 * I understand that copying code is considered as plagiarism.
 *
 * 20031509, 16 May 2022 6:07:11 pm
 */

public class ViewTestScore extends Application{

	/**
	 * @param args
	 */	
	private final ObservableList<Student> ScoreList = FXCollections.observableArrayList();
	private final TableView<Student> tableView = new TableView<>();
	private Button btBack = new Button("Back");
	
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		launch(args);
		
	}
	
	@Override
	public void start(Stage primaryStage)
	{
	
		VBox vbPane = new VBox();
	    Label lbWelcome = new Label("View Test Score");
		vbPane.setSpacing(10);
		vbPane.setPadding(new Insets(10,10,10,10));
		vbPane.setAlignment(Pos.CENTER);
		vbPane.getChildren().addAll(lbWelcome,tableView,btBack);

		primaryStage.setTitle("Test Score Check");
		primaryStage.setWidth(600);
		primaryStage.setHeight(800);

		setTableappearance();
		
		
		fillListWithData();
		tableView.setItems(ScoreList);
        
        // define table column
		TableColumn<Student,String> Name = new TableColumn<>("Name");
		Name.setCellValueFactory(new PropertyValueFactory("Name"));
		
	    TableColumn<Student,Integer> TestScore = new TableColumn<>("Test Score");
	    TestScore.setCellValueFactory(new PropertyValueFactory("score"));   
	   
        
	    tableView.getColumns().addAll(Name,TestScore);
	    
	    //addButtonToTable();
	    
	    Scene mainScene = new Scene(vbPane);
		primaryStage.setScene(mainScene);
		primaryStage.show();
		
		EventHandler<ActionEvent> handleBack = (ActionEvent e) -> (new TestScorePage()).start(new Stage());
		btBack.setOnAction(handleBack);
		
	}
	
    private void setTableappearance() {
    	tableView.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);
    	tableView.setPrefWidth(600);
    	tableView.setPrefHeight(600);
    }
	 private void fillListWithData() {
		String jdbcURL = "jdbc:mysql://localhost/fyp";
		String dbUsername = "root";
		String dbPassword = "";
		DBUtil.init(jdbcURL, dbUsername, dbPassword);
		String sql = "SELECT name,score FROM testscore";
		    
		ResultSet rs = DBUtil.getTable(sql);
		try {
			while (rs.next()) {
				ScoreList.add(new Student(rs.getString("name"),rs.getInt("score")));
				//tableView.getItems().add(new Student(rs.getString("name"),rs.getInt("score")));
				}
			rs.last();
			    
		} catch (SQLException e) {
			System.out.println("SQL Error: " + e.getMessage());
			}
		}
	    
//	 private void addButtonToTable() {
//		 TableColumn<Student, Void> Update = new TableColumn("Update");
//		 Update.setSortable(false);
//		 
//		 TableColumn<Student, Void> Delete = new TableColumn("Delete");
//		 Delete.setSortable(false);
//		 
//		 Callback<TableColumn<Student, Void>, TableCell<Student, Void>> UpdatecellFactory = new Callback<TableColumn<Student, Void>, TableCell<Student, Void>>() {
//			 @Override
//	            public TableCell<Student, Void> call(final TableColumn<Student, Void> param) {
//				 final TableCell<Student, Void> cell = new TableCell<Student, Void>() {
//					 private final Button btn = new Button("Update");
//					 {
//						 btn.setOnAction((ActionEvent event) -> (new UpdateTestScore()).start(new Stage()));
//					 }
//					 @Override
//	                 public void updateItem(Void item, boolean empty) {
//						 super.updateItem(item, empty);
//						 if (empty) {
//							 setGraphic(null);
//							 } else {
//								 setGraphic(btn);
//								 }
//						 }
//					 };
//					 return cell;
//					 }
//			 };
//			 Callback<TableColumn<Student, Void>, TableCell<Student, Void>> DeletecellFactory = new Callback<TableColumn<Student, Void>, TableCell<Student, Void>>() {
//				 @Override
//		            public TableCell<Student, Void> call(final TableColumn<Student, Void> param) {
//					 final TableCell<Student, Void> cell = new TableCell<Student, Void>() {
//						 private final Button btn = new Button("Delete");
//						 {
//							 Student student = getTableView().getItems().get(getIndex());
//							 btn.setOnAction((ActionEvent event) -> DeleteTestScore(student));
//						 }
//						 @Override
//		                 public void updateItem(Void item, boolean empty) {
//							 super.updateItem(item, empty);
//							 if (empty) {
//								 setGraphic(null);
//								 } else {
//									 setGraphic(btn);
//									 }
//							 }
//						 };
//						 return cell;
//						 }
//				 };
//				 Update.setCellFactory(UpdatecellFactory);
//				 Delete.setCellFactory(DeletecellFactory);
//				 tableView.getColumns().addAll(Update,Delete);
//				 }	
//	 
//	 private void DeleteTestScore(Student name)
//	 {
//		 String deleteSQL = "DELETE FROM testscore WHERE name='" + name + "'";
//			int rowsDeleted = DBUtil.execSQL(deleteSQL);
//	 }
	 
//	private void addButtonToTabwdwle() {
//	   // define a simple boolean cell value for the action column so that the column will only be shown for non-empty rows.
//	    Update.setCellValueFactory(new Callback<TableColumn.CellDataFeatures<Student, Boolean>, ObservableValue<Boolean>>() {
//	      @Override 
//	     public ObservableValue<Boolean> call(TableColumn.CellDataFeatures<Student, Boolean> features) {
//	        return new SimpleBooleanProperty(features.getValue() != null);
//	      }
//	    });
//	    
//	    Delete.setCellValueFactory(new Callback<TableColumn.CellDataFeatures<Student, Boolean>, ObservableValue<Boolean>>() {
//		     @Override 
//		 public ObservableValue<Boolean> call(TableColumn.CellDataFeatures<Student, Boolean> features) {
//		        return new SimpleBooleanProperty(features.getValue() != null);
//		      }
//		    });
//	    
//	    
//	    
//	    
//	    
//	    
//	    TestScore.setCellFactory(col -> new IntegerEditingCell());
//	}
//
//    public class IntegerEditingCell extends TableCell<Student, Number> {
//
//        private final TextField textField = new TextField();
//        private final Pattern intPattern = Pattern.compile("-?\\d+");
//
//        public IntegerEditingCell() {
//            textField.focusedProperty().addListener((obs, wasFocused, isNowFocused) -> {
//                if (! isNowFocused) {
//                    processEdit();
//                }
//            });
//            textField.setOnAction(event -> processEdit());
//        }
//
//        private void processEdit() {
//            String text = textField.getText();
//            if (intPattern.matcher(text).matches()) {
//                commitEdit(Integer.parseInt(text));
//            } else {
//                cancelEdit();
//            }
//        }
//
//        @Override
//        public void updateItem(Number score, boolean empty) {
//            super.updateItem(score, empty);
//            if (empty) {
//                setText(null);
//                setGraphic(null);
//            } else if (isEditing()) {
//                setText(null);
//                textField.setText(score.toString());
//                setGraphic(textField);
//            } else {
//                setText(score.toString());
//                setGraphic(null);
//            }
//        }
//
//        @Override
//        public void startEdit() {
//            super.startEdit();
//            Number score = getItem();
//            if (score != null) {
//                textField.setText(score.toString());
//                setGraphic(textField);
//                setText(null);
//            }
//        }
//
//        @Override
//        public void cancelEdit() {
//            super.cancelEdit();
//            setText(getItem().toString());
//            setGraphic(null);
//        }
//
//        @Override
//        public void commitEdit(Number score) {
//            super.commitEdit(score);
//            ((Student)this.getTableRow().getItem()).setScore(score.intValue());
//        }
//    }

//    public static class Student {
//        private StringProperty name = new SimpleStringProperty();
//        private IntegerProperty score = new SimpleIntegerProperty();
//        public Student(String name, int score) {
//            this.setName(name);
//            this.setScore(score);
//        }
//        public StringProperty nameProperty() {
//            return this.name;
//        }
//        public java.lang.String getName() {
//            return this.nameProperty().get();
//        }
//        public void setName(java.lang.String name) {
//            this.nameProperty().set(name);
//        }
//        public IntegerProperty scoreProperty() {
//            return this.score;
//        }
//        public int getScore() {
//            return this.scoreProperty().get();
//        }
//        public void setScore(int score) {
//            this.scoreProperty().set(score);
//        }
//
//    }


}





















//package FYPJavaFX;
//
//import java.sql.ResultSet;
//import java.sql.SQLException;
//
//import javafx.application.Application;
//import javafx.collections.FXCollections;
//import javafx.collections.ObservableList;
//import javafx.event.ActionEvent;
//import javafx.event.EventHandler;
//import javafx.geometry.Insets;
//import javafx.geometry.Pos;
//import javafx.scene.Scene;
//import javafx.scene.control.Button;
//import javafx.scene.control.Label;
//import javafx.scene.control.TableColumn;
//import javafx.scene.control.TableView;
//import javafx.scene.control.cell.PropertyValueFactory;
//import javafx.scene.layout.VBox;
//import javafx.stage.Stage;
//
///**
// * I declare that this code was written by me.
// * I will not copy or allow others to copy my code.
// * I understand that copying code is considered as plagiarism.
// *
// * 20031509, 16 May 2022 6:07:11 pm
// */
//
//public class ViewTestScore extends Application{
//
//	/**
//	 * @param args
//	 */
//	private VBox vbPane = new VBox();
//	private Label lbWelcome = new Label("View Test Score");
//
//	public static TableView<Student> table;
//
//	public TableColumn<Student,String> name;
//	public static TableColumn<Student,String> score;
//	
//	private ObservableList<Student> data;
//	
//
//	public static void main(String[] args) {
//		// TODO Auto-generated method stub
//		launch(args);
//		
//	}
//	
//	public void start(Stage primaryStage)
//	{
//		String jdbcURL = "jdbc:mysql://localhost/fyp";
//		String dbUsername = "root";
//		String dbPassword = "";
//
//		DBUtil.init(jdbcURL, dbUsername, dbPassword);
//		
//	    String sql = "SELECT name,score FROM testscore";
//	    
//	    ResultSet rs = DBUtil.getTable(sql);
//		data = FXCollections.observableArrayList();
//	    try {
//			while (rs.next()) {
//				data.add(new Student(rs.getString("name"), rs.getInt("score")));
//			}
//			rs.last();
//			name.setCellValueFactory(new PropertyValueFactory<Student, String>("name"));
//			score.setCellValueFactory(new PropertyValueFactory<Student, String>("score"));
//			
//			table.setItems(data);
//
//		} catch (SQLException e) {
//			System.out.println("SQL Error: " + e.getMessage());
//		}
//
//		table.getColumns().add(name); 
//		table.getColumns().add(score); 
//		
//		
//		vbPane.setSpacing(10);
//		vbPane.setPadding(new Insets(10,10,10,10));
//		vbPane.setAlignment(Pos.CENTER);
//		vbPane.getChildren().addAll(lbWelcome,table);
//
//		Scene mainScene = new Scene(vbPane);
//		primaryStage.setScene(mainScene);
//		primaryStage.setTitle("Test Score Check");
//		primaryStage.setWidth(600);
//		primaryStage.setHeight(800);
//
//		primaryStage.show();
//		
//
//	}
//
//}

