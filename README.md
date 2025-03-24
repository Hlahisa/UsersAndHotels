# Users Backend API
This project provides a backend API for managing users. It is built using ASP.NET Core and targets .NET 8.0. The API provides endpoints for user-related operations.
## Features
- Retrieve all users.
- Retrieve a user by ID.
- Find the nearest user to a specified hotel location.
- Update user details.
- Delete a user.
## Endpoints
### Users
1. **Get All Users**
   - **Method**: `GET`
   - **Route**: `/users`
   - **Response**: Returns a list of all users.
2. **Get User by ID**
   - **Method**: `GET`
   - **Route**: `/users/{id}`
   - **Response**: Returns the details of a user with the specified ID.
3. **Get Nearest User to Hotel**
   - **Method**: `GET`
   - **Route**: `/users/nearest-hotel-user`
   - **Query Parameters**:
     - `latitude`: Latitude of the hotel.
     - `longitude`: Longitude of the hotel.
   - **Response**: Returns the user closest to the specified hotel coordinates.
4. **Update User**
   - **Method**: `PUT`
   - **Route**: `/users/{id}`
   - **Body**: JSON object containing user details to update.
   - **Response**: Updates the user and returns no content.
5. **Delete User**
   - **Method**: `DELETE`
   - **Route**: `/users/{id}`
   - **Response**: Deletes the user and returns no content.
## Technologies Used
- **Framework**: ASP.NET Core
- **Language**: C# 12.0
- **Target Framework**: .NET 8.0
## Getting Started
1. Clone the repository.
2. Open the solution in Visual Studio Community 2022.
3. Restore NuGet packages.
4. Build the solution.
5. Run the project.
## License
This project is licensed under the MIT License. See the LICENSE file for details.
