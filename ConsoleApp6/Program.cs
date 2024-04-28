using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                /* create connection to database */
                string db = "penjualanPadiBeras";/*nama db*/
                SqlConnection conn = null;
                string strKoneksi = "Data Source = DESKTOP-JO96DR8\\RIZKI_RAMADAN;" +
                    "Initial Catalog = {0};" +
                    "User ID = sa ; Password = indonesia;";
                conn = new SqlConnection(string.Format(strKoneksi, db));
                conn.Open();
                Console.Clear();
                while (true)
                {
                    try
                    {
                        /* main menu */
                        Console.WriteLine("\n<------MENU UTAMA APLIKASI------>");
                        Console.WriteLine("1. Data Barang ");
                        Console.WriteLine("2. Data Transaksi ");
                        Console.WriteLine("3. Data Suplier/Distributor ");
                        Console.WriteLine("4. Data Padi/Beras ");
                        Console.WriteLine("5. exit ");
                        Console.WriteLine("\n enter your choice (1-5): ");
                        char ch = Convert.ToChar(Console.ReadLine());

                        switch (ch)
                        {
                            /*Data Barang*/
                            case '1':
                                {
                                    bool backToMenuBarang = false;
                                    while (!backToMenuBarang)
                                    {
                                        Console.WriteLine("\n<---Data Barang--->");
                                        Console.WriteLine("1. Melihat data barang");
                                        Console.WriteLine("2. Tambah data barang");
                                        Console.WriteLine("3. Hapus data barang");
                                        Console.WriteLine("4. Update data barang");
                                        Console.WriteLine("5. exit");
                                        Console.WriteLine("\n enter your choice (1-5): ");
                                        char chB = Convert.ToChar(Console.ReadLine());

                                        switch (chB)
                                        {
                                            /* read */
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("<-><->Berikut data barang>-<>-<");
                                                    Console.WriteLine();
                                                    pr.READdatabarang(conn);
                                                }
                                                break;

                                            /* create */
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    string id_admin, id_p_b;
                                                    int JumlahBerat;
                                                    while (true)
                                                    {
                                                        Console.WriteLine("masukkan Id Admin:");
                                                        id_admin = Console.ReadLine();
                                                        if (id_admin == "ADM20" | id_admin == "ADM49")
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ID admin hanya ADM20 dan ADM49");
                                                        }
                                                    }
                                                    while (true)
                                                    {
                                                        pr.READpadiberas(conn);
                                                        Console.WriteLine("masukkan ID Padi/Beras:");
                                                        id_p_b = Console.ReadLine();
                                                        if (pr.isValidIdPadiBeras(id_p_b, conn))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ID padi/beras tidak valid. Silakan coba lagi.");

                                                        }
                                                    }
                                                    while (true)
                                                    {
                                                        Console.WriteLine("Masukkan Jumlah Berat:");
                                                        string input = Console.ReadLine();

                                                        if (int.TryParse(input, out JumlahBerat))
                                                        {
                                                            break;

                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Masukkan harus berupa angka bulat.");
                                                        }
                                                    }
                                                    DateTime now = DateTime.Now; // Mendapatkan tanggal dan waktu saat ini
                                                    Console.WriteLine("Tanggal sekarang: " + now);
                                                    try
                                                    {
                                                        pr.insertdatabarang(id_p_b, JumlahBerat, now, id_admin, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("anda tidak memiliki " +
                                                            "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            /* delete */
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    pr.READdatabarang(conn);
                                                    Console.WriteLine("Masukkan id log yang ingin di hapus: ");
                                                    string id_log = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.READselecteddatabarang(id_log, conn);
                                                        Console.WriteLine("Ketik Y/y untuk hapus data ketik N/n untuk cancel");
                                                        char Vdb = Convert.ToChar(Console.ReadLine());

                                                        switch (Vdb)
                                                        {
                                                            case 'y':
                                                            case 'Y':
                                                                pr.deletedatabarang(id_log, conn);
                                                                Console.WriteLine("Data dengan id " + id_log + " Berhasil di hapus");
                                                                break;
                                                            case 'n':
                                                            case 'N':
                                                                Console.WriteLine("Membatalkan oprasi hapus data");
                                                                break;
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nanda tidak memiliki" +
                                                            "akses untuk menambah data atau data yang anda masukkan salah");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;

                                            /* update */
                                            case '4':
                                                {
                                                    pr.READdatabarang(conn);
                                                    Console.WriteLine("\n<---Update Data barang--->");
                                                    Console.WriteLine("Masukkan ID log data barang yang akan diperbarui:");
                                                    string newID_log = Console.ReadLine();
                                                    pr.READselecteddatabarang(newID_log, conn);
                                                    try
                                                    {
                                                        Console.WriteLine("Ketik Y/y untuk update data dan ketik N/n untuk cancel");
                                                        char Vdb = Convert.ToChar(Console.ReadLine());

                                                        switch (Vdb)
                                                        {
                                                            case 'y':
                                                            case 'Y':
                                                                Console.WriteLine("Masukkan Jumlah berat baru (kosongkan jika tidak ingin mengubah):");
                                                                string newJumlahBerat = Console.ReadLine();
                                                                Console.WriteLine("masukkan tanggal dan waktu yang baru(kosongkan jika tidak ingin mengubah):");
                                                                string newTanggal = Console.ReadLine();
                                                                if (!string.IsNullOrEmpty(newJumlahBerat) || !string.IsNullOrEmpty(newTanggal))
                                                                {
                                                                    pr.updatedatabarang(newID_log, newJumlahBerat, newTanggal, conn);
                                                                    Console.WriteLine("Data dengan id " + newID_log + " Berhasil di update");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Tidak ada data baru yang dimasukkan. Data tetap tidak berubah.");
                                                                }
                                                                break;
                                                            case 'n':
                                                            case 'N':
                                                                Console.WriteLine("Membatalkan oprasi hapus data");
                                                                break;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine("Error: " + ex.Message);
                                                    }
                                                }
                                                break;

                                            /* exit */
                                            case '5':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                return;

                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n invalid options");
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            /* End Data Barang*/

                            /*Data Transaksi*/
                            case '2':
                                {
                                    bool backToMenuTransaksi = false;
                                    while (!backToMenuTransaksi)
                                    {
                                        Console.WriteLine("\n<---Data Transaksi--->");
                                        Console.WriteLine("1. Melihat data Transaksi");
                                        Console.WriteLine("2. Tambah data Transaksi");
                                        Console.WriteLine("3. Hapus data Transaksi");
                                        Console.WriteLine("4. Update data Transaksi");
                                        Console.WriteLine("5. exit");
                                        Console.WriteLine("\n enter your choice (1-5): ");
                                        char chT = Convert.ToChar(Console.ReadLine());

                                        switch (chT)
                                        {
                                            /* read */
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("<-><->Berikut data transaksi>-<>-<");
                                                    Console.WriteLine();
                                                    pr.READtransaksi(conn);
                                                }
                                                break;

                                            /* create */
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    string jumlahBerat, TotalTransaksi, ID_pb, ID_sd;
                                                    while (true)
                                                    {
                                                        pr.READpadiberas(conn);
                                                        Console.WriteLine("masukkan ID padi/beras:");
                                                        ID_pb = Console.ReadLine();
                                                        if (!string.IsNullOrWhiteSpace(ID_pb) && pr.isValidIdPadiBeras(ID_pb, conn))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ID padi/beras tidak valid. Silakan coba lagi.");
                                                        }
                                                    }
                                                    while (true)
                                                    {
                                                        pr.READsuplierdist(conn);
                                                        Console.WriteLine("masukkan ID suplier/ distributor:");
                                                        ID_sd = Console.ReadLine();
                                                        if (!string.IsNullOrWhiteSpace(ID_sd))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("ID suplier/ distributor tidak boleh kosong. Silakan coba lagi.");
                                                        }
                                                    }
                                                    while (true)
                                                    {
                                                        Console.WriteLine("masukkan jumlah beras:");
                                                        jumlahBerat = Console.ReadLine();
                                                        if (!string.IsNullOrWhiteSpace(jumlahBerat) && int.TryParse(jumlahBerat, out _))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Jumlah beras harus berupa angka bulat. Silakan coba lagi.");
                                                        }
                                                    }

                                                    while (true)
                                                    {
                                                        Console.WriteLine("masukkan total transaksi Rp- ");
                                                        TotalTransaksi = Console.ReadLine();
                                                        if (!string.IsNullOrWhiteSpace(TotalTransaksi) && decimal.TryParse(TotalTransaksi, out _))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Total transaksi harus berupa nominal uang. Silakan coba lagi.");
                                                        }
                                                    }
                                                    try
                                                    {
                                                        pr.insertTransaksi( jumlahBerat, TotalTransaksi, ID_pb, ID_sd, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("Anda tidak memiliki akses untuk menambah data.");
                                                    }

                                                }
                                                break;

                                            /* delete */
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    pr.READtransaksi(conn);
                                                    Console.WriteLine("Masukkan id transaksi yang ingin dihapus: ");
                                                    string id_Transaksi = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.READselectedTransaksi(id_Transaksi, conn);
                                                        Console.WriteLine("Ketik Y/y untuk hapus data, ketik N/n untuk membatalkan.");
                                                        char Vdb = Convert.ToChar(Console.ReadLine());

                                                        switch (char.ToUpper(Vdb))
                                                        {
                                                            case 'y':
                                                            case 'Y':
                                                                pr.deleteTransaksi(id_Transaksi, conn);
                                                                Console.WriteLine("Data dengan id " + id_Transaksi + " berhasil dihapus.");
                                                                break;
                                                            case 'n':
                                                            case 'N':
                                                                Console.WriteLine("Membatalkan operasi hapus data.");
                                                                break;
                                                            default:
                                                                Console.WriteLine("Pilihan tidak valid.");
                                                                break;
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menghapus data atau data yang Anda masukkan salah.");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;


                                            /* update */
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    pr.READtransaksi(conn);
                                                    Console.WriteLine("Masukkan ID transaksi yang ingin diperbarui:");
                                                    string newID_Transaksi = Console.ReadLine();

                                                    // Menampilkan detail transaksi yang akan diperbarui
                                                    pr.READselectedTransaksi(newID_Transaksi, conn);

                                                    try
                                                    {
                                                        Console.WriteLine("Ketik Y/y untuk update data dan ketik N/n untuk cancel");
                                                        char Vdb = Convert.ToChar(Console.ReadLine());

                                                        switch (char.ToUpper(Vdb))
                                                        {
                                                            case 'Y':
                                                                Console.WriteLine("Masukkan jumlah berat baru (kosongkan jika tidak ingin mengubah):");
                                                                string newJumlahBerat = Console.ReadLine();
                                                                Console.WriteLine("Masukkan total harga baru (kosongkan jika tidak ingin mengubah):");
                                                                string newTotalHarga = Console.ReadLine();
                                                                
                                                                pr.updateTransaksi(newID_Transaksi, newJumlahBerat, newTotalHarga, conn);
                                                                Console.WriteLine($"Data dengan ID transaksi {newID_Transaksi} berhasil diperbarui.");
                                                                break;
                                                            case 'N':
                                                                Console.WriteLine("Membatalkan operasi update data.");
                                                                break;
                                                            default:
                                                                Console.WriteLine("Pilihan tidak valid.");
                                                                break;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine("Error: " + ex.Message);
                                                    }
                                                }
                                                break;


                                            /* exit */
                                            case '5':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                break;

                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n invalid options");
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            /*End Data Transaksi*/

                            /*Data Suplier/Distributor*/
                            case '3':
                                {
                                    bool backToMenuSuplierDistributor = false;
                                    while (!backToMenuSuplierDistributor)
                                    {
                                        Console.WriteLine("\n<---Data Suplier/Distributor--->");
                                        Console.WriteLine("1. Melihat data Suplier/Distributor");
                                        Console.WriteLine("2. Tambah data Suplier/Distributor");
                                        Console.WriteLine("3. Hapus data Suplier/Distributor");
                                        Console.WriteLine("4. Update data Suplier/Distributor");
                                        Console.WriteLine("5. exit");
                                        Console.WriteLine("\n enter your choice (1-5): ");
                                        char chSD = Convert.ToChar(Console.ReadLine());

                                        switch (chSD)
                                        {
                                            /* read */
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("<-><->Berikut data suplier/distributor>-<>-<");
                                                    Console.WriteLine();
                                                    pr.READsuplierdist(conn);
                                                }
                                                break;

                                            /* create */
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("masukkan ID suplier/distributor:");
                                                    string id_s_d = Console.ReadLine();
                                                    Console.WriteLine("masukkan Nama suplier/distributor:");
                                                    string NamaSuplier = Console.ReadLine();
                                                    Console.WriteLine("masukkan Alamat suplier / distributor:");
                                                    string Alamat = Console.ReadLine();
                                                    Console.WriteLine("masukkan No telpon suplier / distributor:");
                                                    string NoTelpon = Console.ReadLine();

                                                    bool isUnique = pr.IsDataUnique2("Suplier_Distributor", "Nama_Suplier", NamaSuplier, "Alamat", Alamat, conn);

                                                    if (isUnique)
                                                    {
                                                        Console.WriteLine($"Nama suplier '{NamaSuplier}' belum ada di dalam tabel.");
                                                        try
                                                        {
                                                            pr.insertsuplierdist(id_s_d, NamaSuplier, Alamat, NoTelpon, conn);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("Error: " + ex.Message);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"Nama suplier '{NamaSuplier}' sudah ada di dalam tabel.");
                                                    }

                                                }
                                                break;

                                            /* delete */
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("hapus data berdasarkan id suplier/ distributor:\n");
                                                    string id_s_d = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.deletesuplierdist(id_s_d, conn);
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nanda tidak memiliki" +
                                                            "akses untuk menambah data atau data yang anda masukkan salah");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;

                                            /* update */
                                            case '4':
                                                {
                                                    Console.WriteLine("\n<---Update suplier distributor--->");
                                                    Console.WriteLine("masukkan ID suplier/distributor yang akan di perbarui:");
                                                    string id_s_d = Console.ReadLine();
                                                    Console.WriteLine("masukkan Nama suplier/distributor:");
                                                    string newNamaSuplier = Console.ReadLine();
                                                    Console.WriteLine("masukkan Alamat suplier / distributor:");
                                                    string newAlamat = Console.ReadLine();
                                                    Console.WriteLine("masukkan No telpon suplier / distributor:");
                                                    string newNoTelpon = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.updatesuplierdist(id_s_d, newNamaSuplier, newAlamat, newNoTelpon, conn);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine("Error: " + ex.Message);
                                                    }
                                                }
                                                break;

                                            /* exit */
                                            case '5':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                return;

                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n invalid options");
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            /*End Data Suplier/Distributor*/

                            /*Data Padi/Beras*/
                            case '4':
                                {
                                    bool backToMenuPadiBerasr = false;
                                    while (!backToMenuPadiBerasr)
                                    {
                                        Console.WriteLine("\n<---Data Padi/Beras--->");
                                        Console.WriteLine("1. Melihat data Padi/Beras");
                                        Console.WriteLine("2. Tambah data Padi/Beras");
                                        Console.WriteLine("3. Hapus data Padi/Beras");
                                        Console.WriteLine("4. Update data Padi/Beras");
                                        Console.WriteLine("5. exit");
                                        Console.WriteLine("\n enter your choice (1-5): ");
                                        char chPB = Convert.ToChar(Console.ReadLine());

                                        switch (chPB)
                                        {
                                            /* read */
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("<-><->Berikut data padi dan beras>-<>-<");
                                                    Console.WriteLine();
                                                    pr.READpadiberas(conn);
                                                }
                                                break;

                                            /* create */
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("masukkan ID padiberas:");
                                                    string idp_b = Console.ReadLine();
                                                    Console.WriteLine("masukkan Jenis Padi/beras:");
                                                    string Jenisp_b = Console.ReadLine();
                                                    Console.WriteLine("masukkan Jumlah Padi/beras:");
                                                    string Jumlahp_b = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insertpadiberas(idp_b, Jenisp_b, Jumlahp_b, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("anda tidak memiliki " +
                                                            "akses untuk menambah data");
                                                    }
                                                }
                                                break;

                                            /* delete */
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Masukkan jenis padi yang ingin di hapus:\n");
                                                    string Jenisp_b = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.deletepadiberas(Jenisp_b, conn);
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nanda tidak memiliki" +
                                                            "akses untuk menambah data atau data yang anda masukkan salah");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;

                                            /* update */
                                            case '4':
                                                {
                                                    Console.WriteLine("\n<---Update Data Padi/Beras--->");
                                                    Console.WriteLine("Masukkan ID Padi/Beras yang akan diperbarui:");
                                                    string idp_b = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Padi/Beras baru:");
                                                    string newJenis = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jumlah Padi/Beras baru:");
                                                    string newJumlah = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.updatepadiberas(idp_b, newJenis, newJumlah, conn);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine("Error: " + ex.Message);
                                                    }
                                                }
                                                break;

                                            /* exit */
                                            case '5':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                return;

                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n invalid options");
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            /*End Data Padi/Beras*/

                            /*exit*/
                            case '5':
                                conn.Close();
                                Console.Clear();
                                Main(new string[0]);
                                return;

                            default:
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n invalid options");
                                }
                                break;

                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("\n check for the value entered");
                    }
                }
            }
        }

        /*funtion padi dan beras*/
        /* func read data */
        public void READpadiberas(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_p_b, Jenis_p_b, Jumlah_p_b FROM Padi_Beras", conn);
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌───────┬─────────────────┬──────────────┐");
            Console.WriteLine("| ID_p_b|Jenis_p_b        | Jumlah_p_b   |");
            Console.WriteLine("├───────┼─────────────────┼──────────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_p_b"],-2} | {r["Jenis_p_b"],-1} | {r["Jumlah_p_b"],-12} |");
            }

            Console.WriteLine("└───────┴─────────────────┴──────────────┘");
            r.Close();
        }

        /* func insert/create data */
        public bool insertpadiberas(string idp_b, string Jenisp_b, string Jumlahp_b, SqlConnection conn)
        {
            try
            {
                string str = "INSERT INTO Padi_Beras (ID_p_b,Jenis_p_b, Jumlah_p_b) VALUES (@id,@jenis, @jumlah)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@id", idp_b));
                cmd.Parameters.Add(new SqlParameter("@jenis", Jenisp_b));
                cmd.Parameters.Add(new SqlParameter("@jumlah", Jumlahp_b));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Berhasil Ditambahkan");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        /* func delete data */
        public void deletepadiberas(string Jenisp_b, SqlConnection conn)
        {
            string str = "";
            str = "DELETE Padi_Beras WHERE Jenis_p_b = @jenis";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("jenis", Jenisp_b)); ;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil di hapus");
        }

        /* func update data */
        public void updatepadiberas(string ID_p_b, string newJenisp_b, string newJumlahp_b, SqlConnection conn)
        {
            try
            {
                string str = "UPDATE Padi_Beras SET Jenis_p_b = @newJenis, Jumlah_p_b = @newJumlah WHERE ID_p_b = @id";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@newJenis", newJenisp_b));
                cmd.Parameters.Add(new SqlParameter("@newJumlah", newJumlahp_b));
                cmd.Parameters.Add(new SqlParameter("@id", ID_p_b));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data padi dan beras Berhasil Diperbarui");
                }
                else
                {
                    Console.WriteLine("Tidak ada data dengan ID yang sesuai");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /* end funtion padi dan beras */

        /*function data barang*/
        /* func read data */
        public void READdatabarang(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_log,ID_p_b,Jumlah_Berat,Tanggal,ID_Admin FROM Data_Barang", conn);
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌──────────┬────────┬──────────────┬────────────┬──────────┐");
            Console.WriteLine("| ID_log   | ID_p_b | Jumlah_Berat | Tanggal    | ID_Admin |");
            Console.WriteLine("├──────────┼────────┼──────────────┼────────────┼──────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_log"],-8} | {r["ID_p_b"],-6} | {r["Jumlah_Berat"],-12} | {((DateTime)r["Tanggal"]).ToString("yyyy-MM-dd"),-10} | {r["ID_Admin"],-8} |");
            }

            Console.WriteLine("└──────────┴────────┴──────────────┴────────────┴──────────┘");
            r.Close();
        }

        /* func insert/create data */
        public void insertdatabarang(string id_p_b, int JumlahBerat, DateTime Tanggal, string id_admin, SqlConnection conn)
        {
            try
            {
                string str = "INSERT INTO Data_Barang (ID_p_b,Jumlah_Berat,Tanggal, ID_Admin) VALUES (@id_p_b, @jumlah,@tanggal, @idadmin)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@id_p_b", id_p_b));
                cmd.Parameters.Add(new SqlParameter("@jumlah", JumlahBerat));
                cmd.Parameters.Add(new SqlParameter("@tanggal", Tanggal));
                cmd.Parameters.Add(new SqlParameter("@idadmin", id_admin));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Berhasil Ditambahkan");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /* func delete data */
        public void deletedatabarang(string id_log, SqlConnection conn)
        {
            string str = "";
            str = "DELETE Data_Barang WHERE ID_log = @id_log";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("ID_log", id_log)); ;
            cmd.ExecuteNonQuery();
        }

        /* func update data */
        public void updatedatabarang(string newID_log, string newJumlahBerat, string newTanggal, SqlConnection conn)
        {
            try
            {
                string str = "UPDATE Data_Barang SET Jumlah_Berat = @newjumlah,Tanggal = @newtanggal WHERE ID_log = @newid_log";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@newjumlah", newJumlahBerat));
                cmd.Parameters.Add(new SqlParameter("@newtanggal", newTanggal));
                cmd.Parameters.Add(new SqlParameter("@newid_log", newID_log));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data barang Berhasil Diperbarui");
                }
                else
                {
                    Console.WriteLine("Tidak ada data dengan ID yang sesuai");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /* end function data barang*/

        /*function suplier & distributor*/
        /* func read data */
        public void READsuplierdist(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_s_d, Nama_Suplier, Alamat, No_Telpon FROM Suplier_Distributor", conn);
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌──────────┬───────────────┬─────────────────┬─────────────────┐");
            Console.WriteLine("| ID_s_d   | Nama_Suplier  |     Alamat      |   No_Telpon     |");
            Console.WriteLine("├──────────┼───────────────┼─────────────────┼─────────────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_s_d"],-8} | {r["Nama_Suplier"],-13} | {r["Alamat"],-15} | {r["No_Telpon"],-10} |");
            }

            Console.WriteLine("└──────────┴───────────────┴─────────────────┴─────────────────┘");
            r.Close();
        }

        /* func insert/create data */
        public void insertsuplierdist(string id_s_d, string NamaSuplier, string Alamat, string NoTelpon, SqlConnection conn)
        {
            try
            {
                conn.Open();
                string str = "INSERT INTO Suplier_Distributor (ID_s_d,Nama_Suplier,Alamat,No_Telpon) VALUES (@id_s_d,@namasup, @alamat,@notelp)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@id_s_d", id_s_d));
                cmd.Parameters.Add(new SqlParameter("@namasup", NamaSuplier));
                cmd.Parameters.Add(new SqlParameter("@alamat", Alamat));
                cmd.Parameters.Add(new SqlParameter("@notelp", NoTelpon));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Berhasil Ditambahkan");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /* func delete data */
        public void deletesuplierdist(string id_s_d, SqlConnection conn)
        {
            string str = "";
            str = "DELETE Suplier_Distributor WHERE ID_s_d = @id_s_d";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("ID_s_d", id_s_d)); ;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil di hapus");
        }

        /* func update data */
        public void updatesuplierdist(string newid_s_d, string newNamaSuplier, string newAlamat, string newNoTelpon, SqlConnection conn)
        {
            try
            {
                string str = "UPDATE Suplier_Distributor SET Nama_Suplier=@newNamaSuplier , Alamat=@newAlamat ,No_Telpon=@newNoTelpon WHERE ID_s_d = @newid_s_d";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@newNamaSuplier", newNamaSuplier));
                cmd.Parameters.Add(new SqlParameter("@newAlamat", newAlamat));
                cmd.Parameters.Add(new SqlParameter("@newNoTelpon", newNoTelpon));
                cmd.Parameters.Add(new SqlParameter("@newid_s_d", newid_s_d));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data suplier/distributor Berhasil Diperbarui");
                }
                else
                {
                    Console.WriteLine("Tidak ada data dengan ID yang sesuai");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /*end function suplier & distributor*/

        /*function transaksi*/
        /* func read data */
        public void READtransaksi(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_Transaksi, Jumlah_Berat, Total_Transaksi, ID_p_b, ID_s_d FROM Transaksi", conn);
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌──────────────┬──────────────┬─────────────────┬──────────┬──────────┐");
            Console.WriteLine("| ID_Transaksi | Jumlah_Berat | Total_Transaksi | ID_p_b   |  ID_s_d  |");
            Console.WriteLine("├──────────────┼──────────────┼─────────────────┼──────────┼──────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_Transaksi"],-12} | {r["Jumlah_Berat"],-12} | {r["Total_Transaksi"],-15} | {r["ID_p_b"],-8} | {r["ID_s_d"],-8} |");
            }

            Console.WriteLine("└──────────────┴──────────────┴─────────────────┴──────────┴──────────┘");
            r.Close();
        }

        /* func insert/create data */
        public void insertTransaksi(string jumlahBerat, string TotalTransaksi, string ID_pb, string ID_sd, SqlConnection conn)
        {
            try
            {
                string str = "INSERT INTO Transaksi (Jumlah_Berat,Total_Transaksi,ID_p_b,ID_s_d) VALUES (@jumlahbrt, @totaltr,@id_pb, @id_sd)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@jumlahbrt", jumlahBerat));
                cmd.Parameters.Add(new SqlParameter("@totaltr", TotalTransaksi));
                cmd.Parameters.Add(new SqlParameter("@id_pb", ID_pb));
                cmd.Parameters.Add(new SqlParameter("@id_sd", ID_sd));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Berhasil Ditambahkan");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /* func delete data */
        public void deleteTransaksi(string id_Transaksi, SqlConnection conn)
        {
            string str = "";
            str = "DELETE Transaksi WHERE ID_Transaksi = @id_transaksi";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("ID_Transaksi", id_Transaksi)); ;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil di hapus");
        }

        /* func update data */
        public void updateTransaksi(string newID_Transaksi, string newJumlahbrt, string newTotaltr, SqlConnection conn)
        {
            try
            {
                string str = "UPDATE Transaksi SET Jumlah_Berat=@newjumlahbrt ,Total_Transaksi=@newtotaltr WHERE ID_Transaksi = @newid_transaksi";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@newjumlahbrt", newJumlahbrt));
                cmd.Parameters.Add(new SqlParameter("@newtotaltr", newTotaltr));
                cmd.Parameters.Add(new SqlParameter("@newid_transaksi", newID_Transaksi));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data transaksi Berhasil Diperbarui");
                }
                else
                {
                    Console.WriteLine("Tidak ada data dengan ID yang sesuai");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /* end function transaksi*/

        /*function for validation*/
        public bool isValidIdPadiBeras(string id, SqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM Padi_Beras WHERE ID_p_b = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
        /*validation delete log*/
        public void READselecteddatabarang(string ID_log, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_log, ID_p_b, Jumlah_Berat, Tanggal, ID_Admin FROM Data_Barang WHERE ID_log = @ID_log", conn);
            cmd.Parameters.Add("@ID_log", SqlDbType.VarChar).Value = ID_log;
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌──────────┬────────┬──────────────┬────────────┬──────────┐");
            Console.WriteLine("| ID_log   | ID_p_b | Jumlah_Berat | Tanggal    | ID_Admin |");
            Console.WriteLine("├──────────┼────────┼──────────────┼────────────┼──────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_log"],-8} | {r["ID_p_b"],-6} | {r["Jumlah_Berat"],-12} | {((DateTime)r["Tanggal"]).ToString("yyyy-MM-dd"),-10} | {r["ID_Admin"],-8} |");
            }

            Console.WriteLine("└──────────┴────────┴──────────────┴────────────┴──────────┘");
            r.Close();
        }

        public void READselectedTransaksi(string ID_Transaksi, SqlConnection conn) 
        {
            SqlCommand cmd = new SqlCommand("SELECT ID_Transaksi, Jumlah_Berat, Total_Transaksi, ID_p_b, ID_s_d FROM Transaksi WHERE ID_Transaksi = @ID_Transaksi", conn);
            cmd.Parameters.Add("@ID_Transaksi", SqlDbType.VarChar).Value = ID_Transaksi;
            SqlDataReader r = cmd.ExecuteReader();

            Console.WriteLine("┌──────────────┬──────────────┬─────────────────┬──────────┬───────────┐");
            Console.WriteLine("| ID_Transaksi | Jumlah_Berat | Total_Transaksi | ID_p_b   |   ID_s_d  |");
            Console.WriteLine("├──────────────┼──────────────┼─────────────────┼──────────┼───────────┤");

            while (r.Read())
            {
                Console.WriteLine($"| {r["ID_Transaksi"],-12} | {r["Jumlah_Berat"],-12} | {r["Total_Transaksi"],-15} | {r["ID_p_b"],-8} | {r["ID_s_d"],-8} |");
            }

            Console.WriteLine("└──────────────┴──────────────┴─────────────────┴──────────┴───────────┘");
            r.Close();
        }

        //validasi 2 data 
        public bool IsDataUnique2(string tableName, string columnName1, string value1, string columnName2, string value2, SqlConnection conn)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName1} = @value1 AND {columnName2} = @value2";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@value1", value1);
                command.Parameters.AddWithValue("@value2", value2);

                try
                {
                    // Pastikan koneksi ditutup jika sudah terbuka sebelumnya
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                    conn.Open();
                    int count = (int)command.ExecuteScalar();

                    // Jika ada data dengan nilai yang sama, return false
                    // Jika tidak ada, return true
                    return count == 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Pastikan untuk menutup koneksi setelah selesai digunakan
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
        //validasi 1 data 
        public bool IsDataUnique1(string tableName, string columnName1, string value1, SqlConnection conn)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName1} = @value1";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@value1", value1);

                try
                {
                    // Pastikan koneksi ditutup jika sudah terbuka sebelumnya
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                    conn.Open();
                    int count = (int)command.ExecuteScalar();

                    // Jika ada data dengan nilai yang sama, return false
                    // Jika tidak ada, return true
                    return count == 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Pastikan untuk menutup koneksi setelah selesai digunakan
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}