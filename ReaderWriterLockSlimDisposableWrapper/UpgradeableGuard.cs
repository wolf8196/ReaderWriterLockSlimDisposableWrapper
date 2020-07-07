using System;
using System.Threading;

namespace ReaderWriterLockSlimDisposableWrapper
{
    public sealed partial class UpgradeableGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim readerWriterLock;
        private UpgradedGuard upgradedLock;

        public UpgradeableGuard(ReaderWriterLockSlim readerWriterLock)
        {
            this.readerWriterLock = readerWriterLock;
            readerWriterLock.EnterUpgradeableReadLock();
        }

        public IDisposable UpgradeToWriterLock()
        {
            if (upgradedLock == null)
            {
                upgradedLock = new UpgradedGuard(this);
            }

            return upgradedLock;
        }

        public void Dispose()
        {
            if (upgradedLock != null)
            {
                upgradedLock.Dispose();
            }

            readerWriterLock.ExitUpgradeableReadLock();
        }
    }
}