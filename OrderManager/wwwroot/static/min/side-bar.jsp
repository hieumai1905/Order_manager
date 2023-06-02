<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html lang="en">
<head>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Manager</title>

    <!-- Google Font: Source Sans Pro -->
    <link
            rel="stylesheet"
            href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback"
    />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../static/css/fontawesome.all.min.css"/>
    <!-- DataTables -->
    <link rel="stylesheet" href="../static/css/dataTables.bootstrap4.min.css"/>
    <link rel="stylesheet" href="../static/css/responsive.bootstrap4.min.css"/>
    <!-- Theme style -->
    <link rel="stylesheet" href="../static/css/adminlte.min.css"/>
</head>
<div class="sidebar">
    <!-- Sidebar Menu -->
    <nav class="mt-2">
        <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu"
            data-accordion="false">
            <li class="nav-item">
                <a href="/products" class="nav-link">
                    <span class="nav-icon badge">P</span>
                    <p>[ProductsPage]</p>
                </a>
            </li>
            <li class="nav-item">
                <a href="/categories" class="nav-link">
                    <span class="nav-icon badge">C</span>
                    <p>[CategoriesPage]</p>
                </a>
            </li>
            <li class="nav-item">
                <a href="/users" class="nav-link">
                    <span class="nav-icon badge">U</span>
                    <p>[UsersPage]</p>
                </a>
            </li>
            <li class="nav-item">
                <a href="/bills?action=view" class="nav-link">
                    <span class="nav-icon badge">B</span>
                    <p>[BillsPage]</p>
                </a>
            </li>
            <li class="nav-item">
                <c:if test="${sessionScope.Member == null}">
                    <a href="/login-signup?action=logout" class="nav-link">
                            <span class="nav-icon badge">
                                <i class="fas fa-sign-in-alt"></i>
                            </span>
                        <p>Login</p>
                    </a>
                </c:if><c:if test="${sessionScope.Member != null}">
                <a href="/login-signup?action=logout" class="nav-link">
                            <span class="nav-icon badge">
                                <i class="fas fa-sign-out-alt"></i>
                            </span>
                    <p>Logout</p>
                </a>
            </c:if>
            </li>
        </ul>
    </nav>
    <!-- /.sidebar-menu -->
</div>
</body>
</html>
