# ASP.NetProject

# Project Title

Bug Tracking System is a web-based application built with ASP.NET Core that allows development teams to manage software bugs efficiently. It supports tracking of bugs per project, assigning bugs to users, uploading attachments, and managing project versions.

## API Reference

### Register User

`POST /api/Users/Register`

| Body     | Type   | Description |
| -------- | ------ | ----------- |
| userName | string | _Required_  |
| userRole | string | _Required_  |
| password | string | _Required_  |
| email    | string | _Required_  |

---

### LogIn

`POST /api/Users/Login`

| Body     | Type   | Description |
| -------- | ------ | ----------- |
| userName | string | _Required_  |
| password | string | _Required_  |

---

### Get All Projects

`GET /api/Projects`

Lists all projects.

---

### Add New Project

`POST /api/Projects`

| Body               | Type   | Description |
| ------------------ | ------ | ----------- |
| projectName        | string | _Required_  |
| projectDescription | string | _Required_  |

---

### View Specific Project Info and Bugs

`GET /api/Projects/{id}`

| Parameter | Type | Description            |
| --------- | ---- | ---------------------- |
| id        | Guid | _Required_. Project ID |

---

### Report a New Bug

`POST /api/Bug`

| Body           | Type   | Description                       |
| -------------- | ------ | --------------------------------- |
| bugName        | string | _Required_                        |
| bugDescription | string | _Required_                        |
| bugType        | string | _Required_                        |
| priority       | string | _Required_                        |
| status         | string | _Required_                        |
| project_Id     | Guid   | _Required_. Project to assign bug |

---

### List All Bugs

`GET /api/Bug`

Returns a list of all bugs.

---

### View Specific Bug Info

`GET /api/Bug/{id}`

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| id        | Guid | _Required_. Bug ID |

---

### Assign a User to a Bug

`POST /api/Bug/{bug_Id}/assignees`

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| bug_Id    | Guid | _Required_. Bug ID |

| Body    | Type   | Description         |
| ------- | ------ | ------------------- |
| user_Id | string | _Required_. User ID |

---

### Unassign a User from a Bug

`DELETE /api/Bug/{bug_Id}/assignees/{user_Id}`

| Parameter | Type | Description         |
| --------- | ---- | ------------------- |
| bug_Id    | Guid | _Required_. Bug ID  |
| user_Id   | Guid | _Required_. User ID |

---

### Upload Attachment to a Bug

`POST /api/Bug/{bug_Id}/attachments`

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| bug_Id    | Guid | _Required_. Bug ID |

| Body | Type | Description |
| ---- | ---- | ----------- |
| file | File | _Required_  |

---

### Get Attachments for a Bug

`GET /api/Bug/{id}/attachments`

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| id        | Guid | _Required_. Bug ID |

---

### Delete an Attachment

`DELETE /api/Bug/{bug_Id}/attachments/{attachment_Id}`

| Parameter     | Type | Description               |
| ------------- | ---- | ------------------------- |
| bug_Id        | Guid | _Required_. Bug ID        |
| attachment_Id | Guid | _Required_. Attachment ID |

---
