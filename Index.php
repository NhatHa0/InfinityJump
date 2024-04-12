<!DOCTYPE html>
<html lang="vi">

<head>
    <meta charset="utf-8">
    <link href="C:\xampp\htdocs\demo1\site.css" rel="stysheet" />
    <title>Hello World Script</title>
</head>

<body>
    <div id="wrapper">

        <!-- Button chuyển sang index.html -->
        <button onclick="goToIndex()">Chuyển sang trang chủ</button>

        <script>
        // Hàm chuyển trang
        function goToIndex() {
            window.location.href = "Game/index.html";
        }
        </script>

    </div>
</body>

</html>