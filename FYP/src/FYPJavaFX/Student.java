package FYPJavaFX;

/**
 * I declare that this code was written by me.
 * I will not copy or allow others to copy my code.
 * I understand that copying code is considered as plagiarism.
 *
 * 20031509, 16 May 2022 6:27:39 pm
 */

public class Student {
	private String name = null;
    private int score = 0;
    
    public Student(String name, int score) {
    	this.name = name;
        this.score = score;
    }

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}


}
