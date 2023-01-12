using LibraryManager;
using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager._2_ApplicationServices.PersonProvider;
using LibraryManager._2_ApplicationServices.PersonProvider.ChangePersonData;
using LibraryManager.BookProvider;
using LibraryManager.BookProvider.ChangeBookData;
using LibraryManager.Data;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.Repositories.ItemInFile;
using LibraryManager.Repositories.PeronInFile;
using LibraryManager.UserComunication;
using LibraryManager.UserComunication.MenuForBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IRepository<PersonComments>, SqlRepository<PersonComments>>();
services.AddSingleton<IBookProvider, BookProvider>();
services.AddSingleton<IPersonProvider, PersonProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IPersonInFile, PersonInFile>();
services.AddSingleton<IItemInFile, ItemInFile>();
services.AddSingleton<IMenuForBooks, MenuForBooks>();
services.AddSingleton<IChangePersonData, ChangePersonData>();
services.AddSingleton<IChangeBookData, ChangeBookData>();
services.AddDbContext<LibraryManagerDbContext>(options => options.
    UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LibraryManager;Integrated Security=True; Encrypt=False"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();

