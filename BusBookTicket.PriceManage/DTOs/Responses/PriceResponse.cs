﻿namespace BusBookTicket.PriceManage.DTOs.Responses;

public class PriceResponse
{
    public int CompanyId { get; set; }
    public int RouteId { get; set; }
    public double Surcharges { get; set; }
    public double Price { get; set; }
    public string CompanyName { get; set; }
}