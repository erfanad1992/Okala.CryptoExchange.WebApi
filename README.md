# Okala.CryptoExchange.WebApi

# Answers to Technical Questions 

## 1. Time Spent on Coding Assignment  
I spent approximately **15 hours** on the coding assignment. If I had more time, I would enhance the solution by:   
- Implementing User Management for Authorization of JwtToken.  
- Integrating logging for better observability.   

## 2. Most Useful Feature in Latest Version of C#  
The most useful feature added in the latest version of C# (C# 10) is **global using directives**. 
This feature allows developers to declare using directives that apply to all files in a project, thus reducing redundancy and improving code readability.  

### Code Snippet  
```csharp  
// In a file called GlobalUsings.cs  
global using System;  
global using System.Collections.Generic;  

// Now you can use System and System.Collections.Generic in any file without needing to declare them again  
public class Example  
{  
    public List<string> GetNames()  
    {  
        return new List<string> { "Alice", "Bob", "Charlie" };  
    }  
}  
```

### 3. Tracking Down Performance Issues in Production  
To track down a performance issue in production, I would take the following steps:  

1. **Use Monitoring Tools**: I would utilize **Kibana** to monitor HTTP requests logs, allowing for efficient visualization and analysis of request data.  

2. **Database Monitoring**: For monitoring database performance, I would use **SQL Profiler** to track and analyze the SQL queries being executed, identifying slow or problematic queries.  

3. **General Service Monitoring**: I would implement **Prometheus** and **Grafana** for comprehensive monitoring of all services and resources. Prometheus would collect metrics, while Grafana would provide powerful visualization capabilities to track system performance over time.  

4. **Load Testing**: For conducting load tests, I would use **k6**, an open-source load testing tool, to simulate traffic and ensure that the application can handle expected user loads.  

Yes, I have had to track down performance issues in the past, particularly when optimizing SQL queries or ensuring that services scale effectively under heavy loads.  

Yes, I have had to track down performance issues in the past, particularly when optimizing SQL queries that were causing slow response times.  

## 4. Latest Technical Book or Conference  
The latest technical book I read is **"Clean Architecture" by Robert C. Martin**. This book emphasizes:  

- The importance of separation of concerns in system architecture.  
- Dependency inversion as a key principle for maintainability.  
- Strategies for designing systems that are flexible, testable, and easy to understand.  

These concepts helped reinforce my understanding of how to structure applications for long-term success.

## 5. Thoughts on the Technical Assessment  
I found the technical assessment to be a valuable and insightful experience. 
It challenged my practical coding skills, particularly in areas such as JWT authentication and token management. 
The tasks were relevant and allowed me to demonstrate both my technical knowledge and my approach to problem-solving.
Overall, it provided a comprehensive view of essential skills for the role.  

## 6. Describe Yourself Using JSON  
```json  
{  
    "name": "erfan darvishniya",  
    "age": 33,  
    "profession": "Software Developer",  
    "skills": [  
        "C#",  
        "Asp.NetCore",
        "OOP",
        "Clean Architecture",
        "CQRS",
        "DDD",
        "MicroService",    
        "EfCore",    
        "RestFulApi",    
        "Event Driven",
        "TSQL",  
        "GRPC",  
        "Docker" , 
        "TDD" , 
        "CAP",  
        "RabbitMQ" ,
        "..."

    ],  
    "interests": [  
        "Technologies",
        "Developments",
        "Machine Learning",  
        "DevOps",

    ],  
    "location": "Iran , Tehran"  
}
```