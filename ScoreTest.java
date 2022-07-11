package test;

import static org.junit.Assert.*;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import FYPJavaFX.AddTestScore;
import FYPJavaFX.DBUtil;
import FYPJavaFX.ViewTestScore;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import FYPJavaFX.TestScorePage;
import FYPJavaFX.Student;

/**
 * I declare that this code was written by me.
 * I will not copy or allow others to copy my code.
 * I understand that copying code is considered as plagiarism.
 *
 * 20031509, 16 May 2022 10:43:47 pm
 */

public class ScoreTest {
	
	// Student Score Test variables
	private Student st1;
	private Student st2;
	private Student st3;
	private final ObservableList<Student> ScoreList = FXCollections.observableArrayList();
	
	public ScoreTest() {
		super();
	}
	@Before
	public void setUp() throws Exception {
		
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
		// prepare test data
		st1 = new Student("Adam",55);
		st2 = new Student("Eve",70);
		st3 = new Student("Tom",2000);
	}

	@Test
	public void testAddStudent() {
		// Score List is not null, so that can add a new item
		assertNotNull("Test if there is valid Student arraylist to add to", ScoreList);

		// check if student score can be added
		ScoreList.add(st1);
		
		assertEquals("Check that ScoreList's size is 2", 2, ScoreList.size());
		assertSame("Check the correct student score is added", st1, ScoreList.get(1));
		
		// check if a second student score can be added
		ScoreList.add(st2);

		assertEquals("Check that ScoreList's size is 3", 3, ScoreList.size());
		assertSame("Check the correct student score is added", st2, ScoreList.get(2));
		
	}

}
