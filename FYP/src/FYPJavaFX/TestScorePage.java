package FYPJavaFX;

import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

/**
 * I declare that this code was written by me.
 * I will not copy or allow others to copy my code.
 * I understand that copying code is considered as plagiarism.
 *
 * 20031509, 16 May 2022 5:44:17 pm
 */

public class TestScorePage extends Application{

	/**
	 * @param args
	 */
	private VBox vbPane = new VBox();
	private Label lbWelcome = new Label("Test Score Application");

	private Button btAdd = new Button("Add");
	private Button btView = new Button("View");
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		launch(args);
		
	}
	
	public void start(Stage primaryStage)
	{
		String jdbcURL = "jdbc:mysql://localhost/demodb";
		String dbUsername = "root";
		String dbPassword = "";

		DBUtil.init(jdbcURL, dbUsername, dbPassword);

		vbPane.setSpacing(10);
		vbPane.setPadding(new Insets(10,10,10,10));
		vbPane.setAlignment(Pos.CENTER);
		vbPane.getChildren().addAll(lbWelcome,btAdd,btView);

		Scene mainScene = new Scene(vbPane);
		primaryStage.setScene(mainScene);
		primaryStage.setTitle("Test Score Application");
		primaryStage.setWidth(600);
		primaryStage.setHeight(800);

		primaryStage.show();
		
		EventHandler<ActionEvent> handleAdd = (ActionEvent e) -> (new AddTestScore()).start(new Stage());
		btAdd.setOnAction(handleAdd);
		
		EventHandler<ActionEvent> handleView = (ActionEvent e) -> (new ViewTestScore()).start(new Stage());
		btView.setOnAction(handleView);
		

	}

}
