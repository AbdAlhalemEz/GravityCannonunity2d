<?php
include('connection.php');

// Get the username and password parameters from the HTTP POST request
$username = $_POST["username"];
$password = $_POST["password"];

// Escape special characters in the username and password to prevent SQL injection
$username = mysqli_real_escape_string($conn, $username);
$password = mysqli_real_escape_string($conn, $password);

// Query the database for the specified username and password
$sql = "SELECT id FROM student WHERE email = '$username' AND password = '$password'";
$result = $conn->query($sql);

// Check for errors in the SQL query
if (!$result) {
    echo "Error: " . $sql . "<br>" . $conn->error;
} 
// Check if any rows were returned
else if ($result->num_rows > 0) {
    // The username and password were correct
    $row = $result->fetch_assoc();
    $id = $row["id"];
    echo "Login successful! $id";
} else {
    // The username and password were incorrect
    echo "Invalid username or password.";
}

// Close the database connection
$conn->close();
?>
