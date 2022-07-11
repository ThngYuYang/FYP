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
 * 20031509, 18 May 2022 4:12:27 pm
 */

public class UpdateTestScore extends Application{

	/**
	 * @param args
	 */
	private VBox vbPane = new VBox();
	private Label lbWelcome = new Label("Update Test Score");
	private Label lbName = new Label("Name");
	private Label lbScore = new Label("Score");

	private Label lbStatus = new Label("");

	private TextField tfName = new TextField();
	private TextField tfScore = new TextField();
	
	private Button btUpdate = new Button("Update Student");
	public static void main(String[] args) {
		// TODO Auto-generated method stub

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
		vbPane.getChildren().addAll(lbWelcome,lbName,tfName,lbScore,tfScore,btUpdate,lbStatus);

		Scene mainScene = new Scene(vbPane);
		primaryStage.setScene(mainScene);
		primaryStage.setTitle("Update Test Score");
		primaryStage.setWidth(600);
		primaryStage.setHeight(800);

		primaryStage.show();
		
		EventHandler<ActionEvent> handleAdd = (ActionEvent e) -> doAdd();
		btUpdate.setOnAction(handleAdd);

	}

	private void doAdd() {
		String name = tfName.getText();
		int score = Integer.parseInt(tfScore.getText());

		String insertSql = "INSERT INTO testscore(name,score) VALUES('" + name + "', " + score + ")";
		int rowsAdded = DBUtil.execSQL(insertSql);

		if (rowsAdded == 1) {

			lbStatus.setText("Test Score Updated!");

		} else {
			lbStatus.setText("Failed To Update Test Score");
		}
		
	}
}

