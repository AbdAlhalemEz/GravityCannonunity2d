<?php
include('connection.php');

$name = $_POST['name'];
$score = $_POST['score'];

// Prepare the SQL statement
$stmt = $conn->prepare("INSERT INTO scores (name, score) VALUES (?, ?)");

// Bind the parameters to the statement
$stmt->bind_param("si", $name, $score);

// Execute the statement
if ($stmt->execute() === TRUE) {
    echo "Score added successfully";
} else {
    echo "Error adding score: " . $conn->error;
}

$stmt->close();
$conn->close();
?>
