using System;
using System.Collections.Generic;

// SOAL 4 - ABSTRACTION
abstract class SewaKendaraan
{
    // SOAL 1 - ENCAPSULATION
    private string namaPenyewa;
    private string idSewa;
    private string tipeMobil;

    // Constructor
    public SewaKendaraan(string namaPenyewa, string idSewa, string tipeMobil)
    {
        this.namaPenyewa = namaPenyewa;
        this.idSewa = idSewa;
        this.tipeMobil = tipeMobil;
    }

    // --- Getter & Setter (Properties) ---
    public string NamaPenyewa
    {
        get { return namaPenyewa; }
        set { namaPenyewa = value; }
    }

    public string IdSewa
    {
        get { return idSewa; }
        set { idSewa = value; }
    }

    public string TipeMobil
    {
        get { return tipeMobil; }
        set { tipeMobil = value; }
    }

    // Metode umum (non-abstrak) — bisa dipakai semua kelas turunan
    public void TampilInfo()
    {
        Console.WriteLine($"Penyewa: {namaPenyewa} | ID: {idSewa} | Mobil: {tipeMobil}");
    }

    // SOAL 3 - POLYMORPHISM
    public abstract double HitungTotalSewa(int lamaHari);
}


// SOAL 2 - INHERITANCE (Kelas Turunan 1)

class SewaRegular : SewaKendaraan
{
    private double hargaPerHari;

    public SewaRegular(string namaPenyewa, string idSewa, string tipeMobil, double hargaPerHari)
        : base(namaPenyewa, idSewa, tipeMobil)
    {
        this.hargaPerHari = hargaPerHari;
    }
    
    public double HargaPerHari
    {
        get { return hargaPerHari; }
        set { hargaPerHari = value; }
    }

    // SOAL 3 - POLYMORPHISM (Override)
    // Rumus: total = lamaHari x hargaPerHari

    public override double HitungTotalSewa(int lamaHari)
    {
        return lamaHari * hargaPerHari;
    }
}


// SOAL 2 - INHERITANCE (Kelas Turunan 2)
class SewaPremium : SewaKendaraan
{
    private double hargaPerHari;
    private double biayaSopir;
    
    public SewaPremium(string namaPenyewa, string idSewa, string tipeMobil,
                       double hargaPerHari, double biayaSopir)
        : base(namaPenyewa, idSewa, tipeMobil)
    {
        this.hargaPerHari = hargaPerHari;
        this.biayaSopir = biayaSopir;
    }

    public double HargaPerHari
    {
        get { return hargaPerHari; }
        set { hargaPerHari = value; }
    }

    public double BiayaSopir
    {
        get { return biayaSopir; }
        set { biayaSopir = value; }
    }

    // SOAL 3 - POLYMORPHISM (Override)

    public override double HitungTotalSewa(int lamaHari)
    {
        return (lamaHari * hargaPerHari) + biayaSopir;
    }
}

// SOAL 5 - TAMBAHAN (Composition)

class RiwayatSewa
{
    // Composition: RiwayatSewa "memiliki" referensi ke SewaKendaraan
    private SewaKendaraan kendaraan;
    private List<(string jenisPaket, int lamaHari, string tanggalSewa)> daftarRiwayat;

    public RiwayatSewa(SewaKendaraan kendaraan)
    {
        this.kendaraan = kendaraan;
        this.daftarRiwayat = new List<(string, int, string)>();
    }

    // Menambah entri riwayat sewa baru
    public void TambahSewa(string jenisPaket, int lamaHari, string tanggalSewa)
    {
        daftarRiwayat.Add((jenisPaket, lamaHari, tanggalSewa));
    }

    // Mencetak seluruh riwayat sewa
    public void CetakRiwayat()
    {
        for (int i = 0; i < daftarRiwayat.Count; i++)
        {
            var r = daftarRiwayat[i];
            Console.WriteLine($"{i + 1}. {r.jenisPaket} | {r.lamaHari} hari | {r.tanggalSewa}");
        }
    }
}


// MAIN PROGRAM — Demonstrasi output sesuai soal

class Program
{
    static void Main(string[] args)
    {
        // Membuat objek SewaPremium (Dika, 2 hari, harga 300000/hari, sopir 50000)
        // Total = (2 x 300000) + 50000 = 650000
        SewaPremium penyewa1 = new SewaPremium("Dika", "R001", "Avanza", 300000, 50000);

        // Menampilkan info penyewa (dari metode umum di kelas induk)
        penyewa1.TampilInfo();

        // Menghitung dan menampilkan total sewa
        int lamaHari = 2;
        double total = penyewa1.HitungTotalSewa(lamaHari);
        Console.WriteLine($"Total Sewa: Rp {total}");

        // Membuat riwayat sewa dan menambahkan entri
        RiwayatSewa riwayat = new RiwayatSewa(penyewa1);
        riwayat.TambahSewa("Premium", lamaHari, "14-10-2025");

        // Mencetak riwayat sewa
        riwayat.CetakRiwayat();
    }
}
