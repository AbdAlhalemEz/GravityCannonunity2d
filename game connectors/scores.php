<?php
include('connection.php');

// Calculate the sum of the 3 scores for each player and retrieve the top 3 scores with the player ID
$sql = "SELECT player_id, SUM(score1 + score2 + score3) AS total_score FROM game_playtime GROUP BY player_id ORDER BY total_score DESC LIMIT 4";
$result = $conn->query($sql);

// Check for errors with the SQL query
if ($result === false) {
    die("Error with query: " . $conn->error);
}

// Create an array to store the top 3 scores and player IDs
$top_scores = array();

// Loop through the results and add each score and player ID to the array
if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        $player_id = $row["player_id"];
        $total_score = $row["total_score"];
        $sql2 = "SELECT fname FROM student WHERE id = $player_id";
        $result2 = $conn->query($sql2);

        // Check for errors with the SQL query
        if ($result2 === false) {
            die("Error with query: " . $conn->error);
        }

        $row2 = $result2->fetch_assoc();
        $fname = $row2["fname"];
        $top_scores[] = array("fname" => $fname, "total_score" => $total_score);
    }
}

// Encode the array as a JSON string
$json_data = json_encode($top_scores);

// Send an HTTP response with the JSON data
header('Content-Type: application/json');
echo $json_data;

// Close the database connection
$conn->close();
?>
