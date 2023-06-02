<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>[ProjectName]</title>

    <!-- Google Font: Source Sans Pro -->
    <link
            rel="stylesheet"
            href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback"
    />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../css/fontawesome.all.min.css"/>
    <!-- DataTables -->
    <link rel="stylesheet" href="../css/dataTables.bootstrap4.min.css"/>
    <link rel="stylesheet" href="../css/responsive.bootstrap4.min.css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../css/adminlte.min.css"/>
</head>
<body>
<nav class="main-header navbar navbar-expand navbar-white navbar-light">
    <!-- Left navbar links -->
    <ul class="navbar-nav">
        <li class="nav-item">
            <a class="nav-link" data-widget="pushmenu" href="#" role="button"
            ><i class="fas fa-bars"></i
            ></a>
        </li>
        <li class="nav-item d-none d-sm-inline-block">
            <a href="/admin/manager.jsp" class="nav-link">Home</a>
        </li>
        <li class="nav-item d-none d-sm-inline-block">
            <a href="#" class="nav-link">Contact</a>
        </li>
    </ul>
<%--    <ul class="navbar-nav">--%>
<%--        <li style="margin-left: 64vw" class="">--%>
<%--            <c:if test="${sessionScope.Member == null}">--%>
<%--                <a href="/login-signup" class="nav-link">Login</a>--%>
<%--            </c:if>--%>
<%--            <c:if test="${sessionScope.Member != null}">--%>
<%--                <a href="/login-signup?action=logout" class="nav-link">Logout</a>--%>
<%--            </c:if>--%>
<%--        </li>--%>
<%--    </ul>--%>
</nav>
</body>
</html>
