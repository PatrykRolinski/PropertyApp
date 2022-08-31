# PropertyApp

The application is designed to view real estate purchase or rent offers from around the world. Users can easily create, update advertisements and search for properties that interest them.

Several user roles have been implemented:
- Users without an account can only view the ads.
- Member has the ability to like ads, thanks to which the ads are placed on a separate list, which make them easier to follow. Morover Member can write messages to the owners of the ad.
- Manager can additionally create and generally manage advertisements, e.g. update the description, add photos, or decide which photo will be displayed as the main one.
- Admins can do anything 😊, e.g. change the role of users.


## Used Design Patterns
- CQRS
- Mediator
- Repository

## Used Technologies
- .NET 6
- ASP.NET Core Web API
- Entity Framework Core 6.0.7
- NLog 5.0.1
- AutoMapper 11.0.0
- MailKit 3.3.0
- MediatR 10.0.1
- CloudinaryDotNet 1.19.0
- FluentValidation 11.1.0
- Bogus 34.0.2


## Example of UI
Exmaple of UI created in Angular is hosted on my other github repository.

![image](https://user-images.githubusercontent.com/92991489/187614711-4a1b7398-f539-432f-a38a-950b1a49faec.png)
