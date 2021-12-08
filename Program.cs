using System;

namespace GuestbookApp
{
    class Program
        // Start av application med körning av filen Guestbook.cs
    {
        static void Main(string[] args)
        {
            Guestbook myGuestbook = new Guestbook();
            myGuestbook.Run();
        }
    }
}
