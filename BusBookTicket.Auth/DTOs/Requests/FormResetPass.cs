﻿namespace BusBookTicket.Auth.DTOs.Requests;

public class FormResetPass
{
    public string username { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string password { get; set; }
}