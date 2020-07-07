using System;

namespace ReaderWriterLockSlimDisposableWrapper
{
    public partial class UpgradeableGuard : IDisposable
    {
        private class UpgradedGuard : IDisposable
        {
            private readonly UpgradeableGuard parentGuard;
            private readonly WriterGuard writerLock;

            public UpgradedGuard(UpgradeableGuard parentGuard)
            {
                this.parentGuard = parentGuard;
                writerLock = new WriterGuard(parentGuard.readerWriterLock);
            }

            public void Dispose()
            {
                writerLock.Dispose();
                parentGuard.upgradedLock = null;
            }
        }
    }
}