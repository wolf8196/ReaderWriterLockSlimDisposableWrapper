using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Xunit;

namespace ReaderWriterLockSlimDisposableWrapper.Test
{
    [ExcludeFromCodeCoverage]
    public class UpgradeableGuardTest
    {
        [Fact]
        public void EntersUpgradeableReadLockOnCreation()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

            // Act
            _ = new UpgradeableGuard(@lock);

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.True(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void ExitsUpgradeableReadLockOnDisposal()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new UpgradeableGuard(@lock);

            // Act
            target.Dispose();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void UpgradesToWriteLock()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new UpgradeableGuard(@lock);

            // Act
            target.UpgradeToWriterLock();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.True(@lock.IsWriteLockHeld);
            Assert.True(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void ExitsUpgradedWriteLockOnDisposal()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new UpgradeableGuard(@lock);
            target.UpgradeToWriterLock();

            // Act
            target.Dispose();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void UpgradesToWriteLockOnce()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new UpgradeableGuard(@lock);

            // Act
            target.UpgradeToWriterLock();
            target.UpgradeToWriterLock();
            target.UpgradeToWriterLock();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.True(@lock.IsWriteLockHeld);
            Assert.True(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void UpgradesToWriteLockViaExternalWriteGuard()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _ = new UpgradeableGuard(@lock);

            // Act
            _ = new WriterGuard(@lock);

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.True(@lock.IsWriteLockHeld);
            Assert.True(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void ExitsWriteLockViaExternalWriteGuard()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _ = new UpgradeableGuard(@lock);
            var target = new WriterGuard(@lock);

            // Act
            target.Dispose();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.True(@lock.IsUpgradeableReadLockHeld);
        }
    }
}