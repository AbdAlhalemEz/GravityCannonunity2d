<?php
include('connection.php');

// Retrieve POST parameters
$playerId = $_POST["playerId"];
$gameId = $_POST["gameId"];

// Check if the record exists
$sql = "SELECT * FROM game_playtime WHERE player_id = '$playerId' AND game_id = '$gameId'";
$result = $conn->query($sql);

if ($result->num_rows == 0) {
    // Record doesn't exist, create a new one
    $timeSpent = $_POST["time_spent"];
    $sql = "INSERT INTO game_playtime (player_id, game_id, score1, score2, score3, score4, score5, time_spent, last_played) VALUES ('$playerId', '$gameId', '0', '0', '0', '0', '0', '$timeSpent', NOW())";
    
    if ($conn->query($sql) === TRUE) {
        echo "Record created successfully";
    } else {
        echo "Error creating record: " . $conn->error;
    }
} else {
    // Record exists, perform updates based on the 'do' parameter
    $do = $_POST["do"];

    if ($do == 1) {
        // Update time spent
        $timeSpent = $_POST["time_spent"];
        $sql = "UPDATE game_playtime SET time_spent = time_spent + '$timeSpent' WHERE player_id = '$playerId' AND game_id = '$gameId'";
        executeQuery($sql);
    } elseif ($do == 2) {
        // Update scores based on the level
        $level = $_POST['level'];
        $score = $_POST['score'];

        // Construct the column name dynamically based on the level
        $scoreColumn = "score" . $level;

        // Retrieve the old score from the database
        $sql = "SELECT $scoreColumn FROM game_playtime WHERE player_id = '$playerId' AND game_id = '$gameId'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            $row = $result->fetch_assoc();
            $oldScore = $row[$scoreColumn];

            // Update the score only if the new score is greater than the old score
            if ($score > $oldScore) {
                $sql = "UPDATE game_playtime SET $scoreColumn = '$score' WHERE player_id = '$playerId' AND game_id = '$gameId'";
                executeQuery($sql);
            } else {
                echo "New score is not greater than the old score";
            }
        }
    } elseif ($do == 3) {
        // Fetch the value of the onoff column from the row with id 1
        $result = mysqli_query($conn, "SELECT onoff FROM control WHERE id=1");
        $row = mysqli_fetch_assoc($result);
        $onoff_value = $row['onoff'];
        
        // Convert the onoff value to an integer and echo it
        $onoff_int = (int)$onoff_value;
        echo $onoff_int;
    }
}

$conn->close();

function executeQuery($sql) {
    global $conn;
    if ($conn->query($sql) === TRUE) {
        echo "Record updated successfully";
    } else {
        echo "Error updating record: " . $conn->error;
    }
}
?>
