﻿using SistemaGestionEntities;
using SistemaGestionUI.Components.Pages.Ventas;

namespace SistemaGestionUI.ClientServices;

public class VentasService
{
    private readonly HttpClient _httpClient;

    public VentasService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Venta>> GetVentas()
    {
        List<Venta>? ventas = await _httpClient.GetFromJsonAsync<List<Venta>>("");
        return ventas;
    }

    public async Task<Venta?> GetOneVenta(int id)
    {
        return await _httpClient.GetFromJsonAsync<Venta>($"{id}");
    }

    public async Task InsertVenta(Venta venta)
    {
        await _httpClient.PostAsJsonAsync("", venta);
    }

    public async Task UpdateVenta(int id, Venta venta)
    {
        await _httpClient.PutAsJsonAsync($"{id}", venta);
    }

    public async Task DeleteVenta(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

}
