using System;
using System.Collections.Generic;
using InsuranceManagementEntity;
using InsuranceManagementDAO;
using InsuranceManagementException;

namespace InsuranceManagementMain
{
    class Program
    {
        static void Main(string[] args)
        {
            InsuranceServiceImpl service = new InsuranceServiceImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Insurance Management System ---");
                Console.WriteLine("1. Create Policy");
                Console.WriteLine("2. View Policy by ID");
                Console.WriteLine("3. View All Policies");
                Console.WriteLine("4. Update Policy");
                Console.WriteLine("5. Delete Policy");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Policy newPolicy = new Policy();
                            Console.Write("Enter Policy ID: ");
                            newPolicy.PolicyId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Policy Name: ");
                            newPolicy.PolicyName = Console.ReadLine();
                            Console.Write("Enter Tenure (in years): ");
                            newPolicy.Tenure = int.Parse(Console.ReadLine());
                            Console.Write("Enter Amount: ");
                            newPolicy.Amount = double.Parse(Console.ReadLine());

                            if (service.CreatePolicy(newPolicy))
                                Console.WriteLine("Policy created successfully.");
                            else
                                Console.WriteLine("Failed to create policy.");
                            break;

                        case 2:
                            Console.Write("Enter Policy ID to view: ");
                            int id = int.Parse(Console.ReadLine());
                            Policy policy = service.GetPolicy(id);
                            Console.WriteLine($"Policy ID: {policy.PolicyId}, Name: {policy.PolicyName}, Tenure: {policy.Tenure}, Amount: {policy.Amount}");
                            break;

                        case 3:
                            List<Policy> policies = service.GetAllPolicies();
                            Console.WriteLine("--- All Policies ---");
                            foreach (Policy p in policies)
                            {
                                Console.WriteLine($"ID: {p.PolicyId}, Name: {p.PolicyName}, Tenure: {p.Tenure}, Amount: {p.Amount}");
                            }
                            break;

                        case 4:
                            Policy updatePolicy = new Policy();
                            Console.Write("Enter Policy ID to update: ");
                            updatePolicy.PolicyId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Policy Name: ");
                            updatePolicy.PolicyName = Console.ReadLine();
                            Console.Write("Enter new Tenure: ");
                            updatePolicy.Tenure = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Amount: ");
                            updatePolicy.Amount = double.Parse(Console.ReadLine());

                            if (service.UpdatePolicy(updatePolicy))
                                Console.WriteLine("Policy updated successfully.");
                            else
                                Console.WriteLine("Failed to update policy.");
                            break;

                        case 5:
                            Console.Write("Enter Policy ID to delete: ");
                            int delId = int.Parse(Console.ReadLine());
                            if (service.DeletePolicy(delId))
                                Console.WriteLine("Policy deleted successfully.");
                            else
                                Console.WriteLine("Failed to delete policy.");
                            break;

                        case 6:
                            exit = true;
                            Console.WriteLine("Exiting the application.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }
                catch (PolicyNotFoundException ex)
                {
                    Console.WriteLine($"[Policy Not Found] {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter valid data.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}
