using System;
using System.Collections.Generic;

namespace DS_Pj2_4
{
    public class PQ<T>
    {
        
        private List<int> liste;
        readonly int boyut;

        public PQ()
            : this(100)
        { }
        public PQ(int s) // Yapılandırıcı
        {
            boyut = s;
            liste = new List<int>();

        }

        public void enque(int item) // Kuyruk sonuna eleman ekler
        {
            liste.Add(item); // sonu arttır ve ekle

        }
        public int remove_musteri()//Minimum ürün sayılı müşteri methodla bulunur ve öncelikli kuyruktan çıkarılır.
        {

            int musteri = min_urun_musteri_bul();
            int urun_sayi = liste[musteri];
  
            liste.Remove(urun_sayi);
            return urun_sayi;
        }
        public bool isEmpty()//Öncelikli kuyruk boş ise true döndürür.
        {
            return (liste.Count == 0);
        }

        public int eleman_say()//Kuyruktaki eleman sayısını döndürür.
        {
            return (liste.Count);
        }

        private int min_urun_musteri_bul()//Minimum ürünlü müşteri bulan method
        {
            int min = int.MaxValue;
            int musteri_index = -1 ;

            for (int musteri = 0 ; musteri < liste.Count ; musteri++ )
            {
                if (liste[musteri] < min)
                {
                    min = liste[musteri];
                    musteri_index = musteri;
                }
            }
            return musteri_index;
        }

    
    } // end class PQ

    class Program
    {

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            int[] urun_say_arr = {6,7,2,1,12,5,3,7,4,2};

            //Müşterilerin ürün sayılarını içeren kuyruk oluşturulur.
            Queue<int> urun_say = new Queue<int>(urun_say_arr);

            int islem_sure;

            int top_bekleme_sure = 0;
            int bekleme_sure = 0;
            int URUNBASISURE = 3;

            int musteri_say = urun_say.Count;

            //Fifo sırası ile kuyruk dolaşılır.
            for (int s=0 ; s<musteri_say ; s++)
            {
                int urunsayi = urun_say.Dequeue();
                //Müşterinin işleminin süresi hesaplanır.
                islem_sure =urunsayi * URUNBASISURE ; 
                //Müşterinin kendi işleminin bitmesi için beklediği süre hesaplanır.
                bekleme_sure += islem_sure;
                //Bulunan veriler ekrana yazdırılır.
                Console.WriteLine("{0}. sıradaki müşterinin işleminin bitmesini bekleme süresi(sn) = {1} ", s+1, bekleme_sure);
                Console.WriteLine("Müşterinin Ürün Sayısı = {0} ", urunsayi);
                //Her müşterinin işleminin bitmesine kadar geçen süreler toplanır.
                top_bekleme_sure += bekleme_sure;
            }
            // Toplanan süreler müşteri sayısına bölünerek bir müşterinin ortalama beklediği süre hesaplanır ve konsola yazdırılır.
            Console.WriteLine("Fifo kuyrukta bekleyen müşteriler için ortalama işlem süresi(sn) =" + ((double)top_bekleme_sure/musteri_say) );
            Console.WriteLine();

            // Aynı işlemin öncelikli kuyrukla yapılması için veriler sıfırlanır.
            bekleme_sure = 0;
            top_bekleme_sure = 0;

            //Öncelikli kuyruk oluşturulur ve veriler bu kuyruğa eklenir.
            PQ<int> pq_urun_say = new PQ<int>();

            foreach(int urun in urun_say_arr)
            {
                pq_urun_say.enque(urun);

            }

            //Müşteri kalmayana kadar müşteri bilgileri ekrana yazdırılır.
            for (int s = 0; s < musteri_say; s++)
            {
                //remove_musteri işlemi minimum ürünlü müşteriyi döndürür.
                int urun_sayi = pq_urun_say.remove_musteri();
                //Müşterinin işlem süresi ve işleminin bitmesi için beklediği süre hesaplanır ve yazdırılır.
                islem_sure = urun_sayi * URUNBASISURE; 
                bekleme_sure += islem_sure;
                Console.WriteLine("{0}. sıradaki müşterinin işleminin bitmesini bekleme süresi(sn) = {1} ", s + 1, bekleme_sure);
                Console.WriteLine("Müşterinin Ürün Sayısı = {0} ", urun_sayi);

                top_bekleme_sure += bekleme_sure;
            }
            //Öncelikli kuyruk sıralamasıyla çalışan işlemler için ortalama değer hesaplanır ve yazdırılır.
            Console.WriteLine("Öncelikli kuyrukta bekleyen müşteriler için ortalama işlem süresi(sn) =" + ((double)top_bekleme_sure / musteri_say));

            Console.ReadKey();
        }
    }
}
