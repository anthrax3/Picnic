# Picnic
Picnic is a .NET Core class library offering simple CMS functionality along with an embedded management interface.  Simply install the package, add a few lines of configuration, and your app is content enabled.  


## Why Another CMS System?
Most of the existing content platforms and frameworks available are either too complicated, too restricted, or not easily used alongside an existing application.  When you have a simple app that requires content management and you don't want to invest in new technology, Picnic saves the day.

### Ok, but what does it do?
It allows you to effortlessly create, manage and render both dynamic pages and inline content.

## Getting Started

#### Step 1: Add the Picnic.Extension Using Directive to your Startup.cs
```csharp
using Picnic.Extensions;
```

#### Step 2: Call AddPicnic(), Specify Store and Setup Authorization
```csharp
public void ConfigureServices(IServiceCollection services)
{            
    //...Existing configuration

    services.AddMvc();
    
    // Add Picnic and Specify Json Store
    services.AddPicnic().UseJsonStore();

    // Configure the Auth Policy
    services.AddAuthorization(options => options.AddPolicy("PicnicAuthPolicy", policyOptions =>
    {
        // NOTE: This example allows anonymous access to the Picnic management interface
        // Add your application's policy specifics to control access to the Picnic interface
        policyOptions.RequireAssertion(x => true);
        policyOptions.Build();
    }));
}
```

There is also an Entity Framework store that can be used like this:
```csharp
services.AddPicnic().UseEFStore<PicnicDbContext>();
```

Replace PicnicDbContext with your custom context if needed and make sure to perform other required steps to setup Entity Framework.

#### Step 3: Add the Picnic Catch-All Route for Dynamic Pages
```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    //...Existing configuration

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

        // Add the Picnic Catch-All Route to enable dynamic pages
        routes.AddPicnicDynamicPageRoute();
    });
}
```

#### How to Access the Admin Interface
Open a browser and go the following Url relative to your application root:
```
/picnic
```

If you want to customize the root part of the path, you can do so when you call AddPicnic() like so:

```csharp
services.AddPicnic(opts => opts.Manage.RoutePrefix = "cms").UseJsonStore();
```

#### How to Render Inline Content
Use the HtmlHelper extension method, Content() from within your razor View:
```csharp
@Html.Content("YourContentKey")
```

## Where can I get it?
Install from [Nuget](https://www.nuget.org/packages/Picnic/) 
```
Install-Package Picnic
```

## License, etc.
Picnic is copyright © 2017 Matthew Marksbury and other contributors under the MIT license.


## Roadmap
Coming soon.

